
namespace UTILCommon.Models {

    public class PagerInfo {

        private int _pageNumber;
        private int _skip;
        private int _rowsPage;

        public int pageNumber {
            get => _pageNumber > 0? _pageNumber : 1;
            set => this._pageNumber = value;
        }

        public int rowsPage {
            get => _rowsPage == 0? 50 : _rowsPage;
            set => this._rowsPage = value;
        }

        public int skip {
            get => this.pageNumber <= 1 ? 0: this.pageNumber * this.rowsPage;
            set => this._skip = value;
        }


        public int pageTotal;

        public long rowsTotal;

        /// <summary>
        /// 
        /// </summary>
        public PagerInfo( int _pageSize) {

            rowsPage = _pageSize;

        }

        public PagerInfo( int _pageSize, int _nroPage): this(_pageSize) {

            pageNumber = _nroPage;

        }
    }
}
