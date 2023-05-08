using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfTestTask.DataAccess;
using WpfTestTask.Model;

namespace WpfTestTask.ViewModel
{
    public class UserInfoViewModel
    {
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;
        private UserInfoRepository _repository;
        private UserInfo _userEntity = null;
        public UserInfoRecord UserRecord { get; set; }

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                    _resetCommand = new RelayCommand(param => ResetData(), null);

                return _resetCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveData(), null);

                return _saveCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                    _editCommand = new RelayCommand(param => EditData((int)param), null);

                return _editCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(param => DeleteUser((int)param), null);

                return _deleteCommand;
            }
        }

        public UserInfoViewModel()
        {
            _userEntity = new UserInfo();
            _repository = new UserInfoRepository();
            UserRecord = new UserInfoRecord();
            GetAll();
        }

        public void ResetData()
        {
            UserRecord.AppName = string.Empty;
            UserRecord.Id = 0;
            UserRecord.UserName = string.Empty;
            UserRecord.Comment = string.Empty;
        }

        public void DeleteUser(int id)
        {
            if (MessageBox.Show("Confirm delete of this record?", "User", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                try
                {
                    var model = _repository.Get(id);
                    _repository.RemoveUser(model);
                    MessageBox.Show("Record successfully deleted."); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                }
            }
        }

        public void SaveData()
        {
            if (UserRecord != null)
            {
                _userEntity.userAppName = UserRecord.AppName;
                _userEntity.userName = UserRecord.UserName;
                _userEntity.userComment = UserRecord.Comment;

                try
                {
                    if (UserRecord.Id <= 0)
                    {
                        _repository.AddUser(_userEntity);
                        MessageBox.Show("New record successfully saved.");
                    }
                    else
                    {
                        _userEntity.userId = UserRecord.Id;
                        _repository.UpdateUser(_userEntity);
                        MessageBox.Show("Record successfully updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while saving. " + ex.InnerException);
                }
                finally
                {
                    GetAll();
                    ResetData();
                }
            }
        }

        public void EditData(int id)
        {
            var model = _repository.Get(id);

            UserRecord.Id = model.userId;
            UserRecord.AppName = model.userAppName;
            UserRecord.UserName = model.userName;
            UserRecord.Comment = model.userComment;

        }

        public void GetAll()
        {
            UserRecord.UserRecords = new ObservableCollection<UserInfoRecord>();
            if (_repository.GetAll() != null)
            {
                _repository.GetAll().ForEach(data => UserRecord.UserRecords.Add(new UserInfoRecord()
                {
                    Id = data.userId,
                    AppName = data.userAppName,
                    UserName = data.userName,
                    Comment = data.userComment
                }));
            }
        }
    }
}