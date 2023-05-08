using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestTask.ViewModel;

namespace WpfTestTask.Model
{
    public class UserInfoRecord : ViewModelBase
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private string _appName;
        public string AppName
        {
            get
            {
                return _appName;
            }
            set
            {
                _appName = value;
                OnPropertyChanged("AppName");
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged("Comment");
            }
        }

        private ObservableCollection<UserInfoRecord> _userRecords;
        public ObservableCollection<UserInfoRecord> UserRecords
        {
            get
            {
                return _userRecords;
            }
            set
            {
                _userRecords = value;
                OnPropertyChanged("UserRecords");
            }
        }
    }
}