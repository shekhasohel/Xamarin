using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace TodoList
{
	public class TodoItemListViewModel : INotifyPropertyChanged
	{
		ObservableCollection<TodoItemViewModel> _items;
		public ObservableCollection<TodoItemViewModel> Items
		{
			get
			{
				if (_items == null)
				{
					_items = new ObservableCollection<TodoItemViewModel>();
					_items = TodoItems.Items;
				}

				return _items;
			}
			set
			{
				_items = value;
				OnPropertyChanged("Items");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		TodoItemViewModel _selectedItem;
		public TodoItemViewModel SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				_selectedItem = value;

				if (_selectedItem == null)
					return;

				UpdateTodoItemCommand.Execute(_selectedItem);

				SelectedItem = null;
			}
		}

		public ICommand NewTodoItemCommand { get; private set;}

		public ICommand UpdateTodoItemCommand { get; private set; }

		public TodoItemListViewModel()
		{
			NewTodoItemCommand = new Command(AddNewItem);
			UpdateTodoItemCommand = new Command<TodoItemViewModel>(UpdateItem);
		}

		static TodoItemListViewModel()
		{
			//fill up default items
			//Items = TodoItems.Items;
		}

		void AddNewItem()
		{
			TodoItemViewModel Item = new TodoItemViewModel();
			Items.Add(Item);
			Application.Current.MainPage.Navigation.PushModalAsync(new TodoItemDetail(Item));
		}

		void UpdateItem(TodoItemViewModel item)
		{
			Application.Current.MainPage.Navigation.PushModalAsync(new TodoItemDetail(item));
		}
	}
}
