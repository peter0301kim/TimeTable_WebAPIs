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

            //_container.Register<IMfdSolutionService, PapercutService>();
        }

        public static void UpdateInterfaceModeDependencies(InterfaceSettings interfaceSettings)
        {

            if (interfaceSettings.InterfaceMode == InterfaceMode.Mock)
            {
                _container.Register<ILecturerService, LecturerMockService>(new LecturerMockService(interfaceSettings));
                _container.Register<ILecturerTimeTableService, LecturerTimeTableMockService>(new LecturerTimeTableMockService(interfaceSettings));
                _container.Register<ISubjectService, SubjectMockService>(new SubjectMockService(interfaceSettings));
                _container.Register<ITimeTableService, TimeTableMockService>(new TimeTableMockService(interfaceSettings));


            }
            else if(interfaceSettings.InterfaceMode == InterfaceMode.MsSql)
            {
                _container.Register<ILecturerService, LecturerMsSqlService>(new LecturerMsSqlService(interfaceSettings));
                _container.Register<ILecturerTimeTableService, LecturerTimeTableMsSqlService>(new LecturerTimeTableMsSqlService(interfaceSettings));
                _container.Register<ISubjectService, SubjectMsSqlService>(new SubjectMsSqlService(interfaceSettings));
                _container.Register<ITimeTableService, TimeTableMsSqlService>(new TimeTableMsSqlService(interfaceSettings));
            }
            else if (interfaceSettings.InterfaceMode == InterfaceMode.MySql)
            {
                _container.Register<ILecturerService, LecturerMySqlService>(new LecturerMySqlService(interfaceSettings));
                _container.Register<ILecturerTimeTableService, LecturerTimeTableMySqlService>(new LecturerTimeTableMySqlService(interfaceSettings));
                _container.Register<ISubjectService, SubjectMySqlService>(new SubjectMySqlService(interfaceSettings));
                _container.Register<ITimeTableService, TimeTableMySqlService>(new TimeTableMySqlService(interfaceSettings));

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
