﻿using AutoProjectWPF.ViewModel.Repositories;
using Model;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace AutoProjectWPF.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        private string autoQuery = "select * from AutoConfig ac inner join AutoType at on ac.CarTypeId = at.id";
        RealizeCacheRepository realize;

        private ObservableCollection<Car> carCollection;
        public ObservableCollection<Car> CarCollection
        {
            get { return carCollection; }
            set
            {
                carCollection = value;
                OnPropertyChange();
            }
        }

        private ObservableCollection<CarType> carTypeCollection;
        public ObservableCollection<CarType> CarTypeCollection
        {
            get { return carTypeCollection; }
            set
            {
                carTypeCollection = value;
                OnPropertyChange();
            }
        }

        public MainViewModel()
        {
            realize = new RealizeCacheRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString); 
            CarCollection = new ObservableCollection<Car>(realize.Load(autoQuery));
            carTypeCollection = ReturnCollectionOfTypes();
        }

        private Car selectedCar;
        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChange();
            }
        }

        /*
         * Command to Create Item
         * Добавляет пустой элемент для редактирование
         * и дальнейшего сохранения (Command Save)
         */
        private ActionViewModel createItem;
        public ActionViewModel CreateItem
        {
            get
            {
                return createItem ??
                    (createItem = new ActionViewModel(obj =>
                    {
                        var car = new Car();
                        carCollection.Add(car);
                        SelectedCar = car;
                    }));
            }
        }

        /*
         * Command to Save Item 
         * Не реализовано по заданию
         */
        private ActionViewModel saveItem;
        public ActionViewModel SaveItem
        {
            get
            {
                return saveItem ??
                    (saveItem = new ActionViewModel(obj =>
                    {
                    }));
            }
        }

        /*
         * Command to Remove Item from Collection
         * Удаляет элемент из коллекции
         */
        private ActionViewModel removeItem;
        public ActionViewModel RemoveItem
        {
            get
            {
                return removeItem ??
                    (removeItem = new ActionViewModel(obj =>
                    {
                        if (obj is Car car)
                        {
                            CarCollection.Remove(car);
                        }
                    }));
            }
        }  
        
        /* Инициализирует коллекцию carTypeCollection,
         * связь машина -> тип,
         * где тип без повторений.
         */
        private ObservableCollection<CarType> ReturnCollectionOfTypes()
        {
            return new ObservableCollection<CarType>
            {
                CarType.Car,
                CarType.PassengerCar,
                CarType.SportCar,
                CarType.Tipper,
                CarType.TruckCar
            };
        }

    }
}
