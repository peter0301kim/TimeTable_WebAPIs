using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableWebAPI.Services.LecturerService;
using TimeTableWebAPI.Services.LecturerTimeTable;
using TimeTableWebAPI.Services.SubjectService;
using TimeTableWebAPI.Services.TimeTableService;
using TinyIoC;

namespace TimeTableWebAPI
{
    public static class DependencyInjector
    {
        private static TinyIoCContainer _container;

        static DependencyInjector()
        {
            _container = new TinyIoCContainer();
        }

        public static void UpdateInterfaceModeDependencies(DataConnectionSettings dataConnectionSettings)
        {

            if (dataConnectionSettings.DataConnectionMode == DataConnectionMode.Mock)
            {
                _container.Register<ILecturerService, LecturerMockService>(new LecturerMockService(dataConnectionSettings));
                _container.Register<ILecturerTimeTableService, LecturerTimeTableMockService>(new LecturerTimeTableMockService(dataConnectionSettings));
                _container.Register<ISubjectService, SubjectMockService>(new SubjectMockService(dataConnectionSettings));
                _container.Register<ITimeTableService, TimeTableMockService>(new TimeTableMockService(dataConnectionSettings));


            }
            else if(dataConnectionSettings.DataConnectionMode == DataConnectionMode.MsSql)
            {
                _container.Register<ILecturerService, LecturerMsSqlService>(new LecturerMsSqlService(dataConnectionSettings));
                _container.Register<ILecturerTimeTableService, LecturerTimeTableMsSqlService>(new LecturerTimeTableMsSqlService(dataConnectionSettings));
                _container.Register<ISubjectService, SubjectMsSqlService>(new SubjectMsSqlService(dataConnectionSettings));
                _container.Register<ITimeTableService, TimeTableMsSqlService>(new TimeTableMsSqlService(dataConnectionSettings));
            }
            else if (dataConnectionSettings.DataConnectionMode == DataConnectionMode.MySql)
            {
                _container.Register<ILecturerService, LecturerMySqlService>(new LecturerMySqlService(dataConnectionSettings));
                _container.Register<ILecturerTimeTableService, LecturerTimeTableMySqlService>(new LecturerTimeTableMySqlService(dataConnectionSettings));
                _container.Register<ISubjectService, SubjectMySqlService>(new SubjectMySqlService(dataConnectionSettings));
                _container.Register<ITimeTableService, TimeTableMySqlService>(new TimeTableMySqlService(dataConnectionSettings));

            }
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
