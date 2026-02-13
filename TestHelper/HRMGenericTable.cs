using Microsoft.Playwright;


namespace NunitPlayV2.TestHelper
{



    public enum ActionType
    {
        Delete,
        Edit
    }

    public class HRMGenericTable
    {

        private readonly IPage _page;
        private readonly ILocator _table;
        private Dictionary<string, int> _headerIndexMap;

        public HRMGenericTable(IPage page)
        {
            _page = page;
            _table = page.Locator("div.oxd-table[role='table']");
        }

        /* =========================
           LOCATORS
           ========================= */

        private ILocator HeaderCells => _table.Locator("div[role='columnheader']");
        private ILocator DataRows => _table.Locator("div.oxd-table-body div[role='row']");

        /* =========================
           HEADER RESOLUTION
           ========================= */

        private async Task BuildHeaderIndexAsync()
        {
            if (_headerIndexMap != null)
                return;

            _headerIndexMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            // Checkbox column (virtual)
            _headerIndexMap["__checkbox__"] = 0;

            int headerCount = await HeaderCells.CountAsync();

            for (int i = 0; i < headerCount; i++)
            {
                string rawText = await HeaderCells.Nth(i).InnerTextAsync();
                string cleanText = NormalizeHeaderText(rawText);

                // +1 because checkbox column is index 0
                _headerIndexMap[cleanText] = i + 1;
            }
        }

        private string NormalizeHeaderText(string text)
        {
            return Regex.Replace(text, @"\s+", " ").Trim();
        }

        public async Task<int> GetColumnIndexAsync(string headerName)
        {
            await BuildHeaderIndexAsync();

            if (!_headerIndexMap.ContainsKey(headerName))
                throw new Exception($"Header '{headerName}' not found.");

            return _headerIndexMap[headerName];
        }

        /* =========================
           CELL ACCESS
           ========================= */

        public async Task<ILocator> GetCellAsync(int rowIndex, string headerName)
        {
            int colIndex = await GetColumnIndexAsync(headerName);

            return DataRows
                .Nth(rowIndex)
                .Locator("div[role='cell']")
                .Nth(colIndex);
        }

        public async Task<string> GetCellTextAsync(string searchHeader, string searchValue, string targetHeader)
        {
            int rowIndex = await FindRowIndexAsync(searchHeader, searchValue);
            return await (await GetCellAsync(rowIndex, targetHeader)).InnerTextAsync();
        }

        /* =========================
           ROW SEARCH
           ========================= */

        public async Task<int> FindRowIndexAsync(string headerName, string expectedText)
        {
            int colIndex = await GetColumnIndexAsync(headerName);
            int rowCount = await DataRows.CountAsync();

            for (int i = 0; i < rowCount; i++)
            {
                string cellText = await DataRows
                    .Nth(i)
                    .Locator("div[role='cell']")
                    .Nth(colIndex)
                    .InnerTextAsync();

                if (cellText.Trim().Equals(expectedText, StringComparison.OrdinalIgnoreCase))
                    return i;
            }

            throw new Exception($"Row with {headerName}='{expectedText}' not found.");
        }

        /* =========================
           COMMON ACTIONS
           ========================= */

        public async Task SelectRowCheckbox(string headerName, string value)
        {
            int rowIndex = await FindRowIndexAsync(headerName, value);

            await DataRows
                .Nth(rowIndex)
                .Locator("input[type='checkbox']")
                .ClickAsync();
        }

        public async Task ClickAction(string headerName, string value, ActionType action)
        {
            int rowIndex = await FindRowIndexAsync(headerName, value);

            string iconSelector = action switch
            {
                ActionType.Delete => "i.bi-trash",
                ActionType.Edit => "i.bi-pencil-fill",
                _ => throw new ArgumentOutOfRangeException()
            };

            await DataRows
                .Nth(rowIndex)
                .Locator(iconSelector)
                .ClickAsync();
        }
    }
}
