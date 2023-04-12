using FluentValidation;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Models.Context;
using SIVIGILA.Repository;
using SIVIGILA.Service.LineaService.Validation;
using SIVIGILA.Service.LineaService;
using SIVIGILA.Service.MetaService.Validation;
using SIVIGILATests.AdminSQLITE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIVIGILA.Service.MetaService;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Models.Entities;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace SIVIGILATests.ServiceTest.MetaTests
{
    public class MetaTests
    {
        private IValidator<MetaDTO> _validator;
        private ConnectionFactory factory = new();
        private context _context;
        private MetaService _service;

        [SetUp]
        public async Task SetUp()
        {
            if (_context != null)
                _context.Dispose();
            //Get the instance of BlogDBContext
            _context = factory.CreateContextForSQLite();
            var repository = new MetaRepository(_context);
            var vigencia = new VigenciaRepository(_context);
            await _context.Set<Estado>().AddAsync(new Estado()
            {
                Tipo = "Vigencia",
                Descripcion = "Prueba"
            });
            await _context.SaveChangesAsync();

            await vigencia.AddRangeAsync(new List<Vigencia>()
            {
                new Vigencia()
                {
                    FechaInicio=DateTime.Now,
                    FechaFin=DateTime.Now.AddDays(1),
                    Presupuesto="Presupuesto 1",
                    Estado_Vigencia_ID=1,
                    Estado=true
                    
                },
                new Vigencia()
                {
                    FechaInicio=DateTime.Now,
                    FechaFin=DateTime.Now.AddDays(2),
                    Presupuesto="Presupuesto 1",
                    Estado_Vigencia_ID=1,
                    Estado=true
                }
            });
            var actividad = new ActividadRepository(_context);
            _validator = new MetaValidator(repository,vigencia,actividad);
            _service = new MetaService(repository, _validator);
        }
        #region AddAsyncRegion

        [Test]
        public void AddAsync_With_No_Zero_ID_TEST()
        {
            var dto = new MetaDTO()
            {
                MetaId = 1,
                NombreMeta = "Meta 1",
                Actividades = null,
                VigenciaID=1
            };
            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.AddAsync(dto));

        }
        [Test]
        public void AddAsync_With_No_Existing_Vigencia_TEST()
        {
            var dto1 = new MetaDTO()
            {
                NombreMeta = "Meta 1",
                Actividades = null,
                VigenciaID = 50
            };
            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.AddAsync(dto1));
        }
        [Test]
        public async Task AddAsync_ValidValues_TEST()
        {
            var dto = new MetaDTO()
            {
                NombreMeta = "Meta 1",
                DetalleMeta= "Detalle por defecto",
                Actividades = null,
                VigenciaID = 1
            };
            int data = await _service.AddAsync(dto);
            Assert.That(data, Is.EqualTo(1));
        }
        #endregion

        #region CreateOrUpdateRangeTests
        [Test]
        public async Task CreateOrUpdateRange_Duplicades_IDS_Test()
        {
            var data = new List<MetaDTO>()
            {
                new MetaDTO()
                {
                    MetaId = 1,
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 1,
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                }
            };
            await _service.AddAsync(new MetaDTO()
            {
                MetaId = 0,
                NombreMeta = "Meta 1",
                Actividades = null,
                DetalleMeta = "Detalle por defecto",
                VigenciaID = 1
            });

            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.CreateOrUpdateRangeAsync(data));
        }

        [Test]
        public async Task CreateOrUpdateRange_With_No_Existing_IDs_Test()
        {
            var data = new List<MetaDTO>()
            {
                new MetaDTO()
                {
                    MetaId = 1,
                    NombreMeta = "Meta 1",
                    DetalleMeta= "Detalle por defecto",
                    Actividades = null,
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 1,
                    NombreMeta = "Meta 1",
                    DetalleMeta= "Detalle por defecto",
                    Actividades = null,
                    VigenciaID=10
                }
            };
            await _service.AddAsync(new MetaDTO()
            {
                MetaId = 0,
                NombreMeta = "Meta 1",
                DetalleMeta = "Detalle por defecto",
                Actividades = null,
                VigenciaID = 1
            });

            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.CreateOrUpdateRangeAsync(data));
        }

        [Test]
        public async Task CreateOrUpdateRange_With_No_Existing_ID_Test()
        {
            var data = new List<MetaDTO>()
            {
                new MetaDTO()
                {
                    MetaId = 1,
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 10,    //No existe la meta
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                }
            };
            await _service.AddAsync(new MetaDTO()
            {
                MetaId = 0, //Se Guarda con 1
                NombreMeta = "Meta 1",
                Actividades = null,
                DetalleMeta = "Detalle por defecto",
                VigenciaID = 1
            });

            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.CreateOrUpdateRangeAsync(data));
        }

        [Test]
        public async Task CreateOrUpdateRange_Valid_Test()
        {
            var data = new List<MetaDTO>()
            {
                new MetaDTO()
                {
                    MetaId = 1,
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 0,
                    NombreMeta = "Meta 2",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=2
                }
            };
            await _service.AddAsync(new MetaDTO()
            {
                MetaId = 0,
                NombreMeta = "Meta 1",
                Actividades = null,
                DetalleMeta = "Detalle por defecto",
                VigenciaID = 1
            });

            //Liberamos el Tracking de la entidad
            var undetachedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in undetachedEntriesCopy)
                entry.State = EntityState.Detached;

            bool response = await _service.CreateOrUpdateRangeAsync(data);
            Assert.IsTrue(response);
        }

        #endregion

        #region ExistTest
        [Test]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        public async Task ExistByIDAsync_Tests(int id, bool EspectedResponse)
        {
            var list = new List<MetaDTO>()
            {
                new MetaDTO()
                {
                    MetaId = 0,
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 0,
                    NombreMeta = "Meta 2",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 0,
                    NombreMeta = "Meta 3",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=2
                }
            };
            await _service.CreateOrUpdateRangeAsync(list);

            bool data = await _service.ExistByIDAsync(id);
            Assert.That(data, Is.EqualTo(EspectedResponse));
        }
        #endregion

        #region GetAllTests
        [Test]
        public async Task GetAllAsync_Test()
        {
            var list = new List<MetaDTO>()
            {
                new MetaDTO()
                {
                    MetaId = 0,
                    NombreMeta = "Meta 1",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 0,
                    NombreMeta = "Meta 2",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=1
                },
                 new MetaDTO
                 {
                    MetaId = 0,
                    NombreMeta = "Meta 3",
                    Actividades = null,
                    DetalleMeta= "Detalle por defecto",
                    VigenciaID=2
                }
            };
            await _service.CreateOrUpdateRangeAsync(list);

            var data = await _service.GetAllAsync();
            Assert.That(data.Count(), Is.EqualTo(list.Count));   //Como las metas no tienen estado, el método trae todas las metas sin excepción

        }
        #endregion

        #region GetByIdTests
        /// <summary>
        /// NOTA: ESTOS TEST FALLAN YA QUE SQLITE NO SOPORTA PROYECCIONES DE PROYECCIÓNES
        /// </summary>
        /// <returns></returns>

        //[Test]  
        //public async Task GetByIDAsync_No_Existing_ID_Test()
        //{
        //    var dto = new MetaDTO()
        //    {
        //        MetaId = 0,
        //        NombreMeta = "Meta 3",
        //        DetalleMeta = "Detalle por defecto",
        //        VigenciaID = 1
        //    };
        //    await _service.AddAsync(dto);   //Se inserta con el cero

        //    Assert.ThrowsAsync<NotFoundException>(async () => await _service.GetByIdAsync(50));

        //}
        //[Test]
        //public async Task GetByIDAsync_Existing_ID_Test()
        //{
        //    var dto = new MetaDTO()
        //    {
        //        MetaId = 0,
        //        NombreMeta = "Meta 3",
        //        Actividades = null,
        //        DetalleMeta = "Detalle por defecto",
        //        VigenciaID = 2
        //    };
        //    await _service.AddAsync(dto);   //Se inserta con el cero

        //    var data = await _service.GetByIdAsync(1);
        //    //Validamos que sean iguales ambos objetos a traves de la serialziación
        //    dto.MetaId = 1;
        //    var antiguoJSON = JsonSerializer.Serialize(dto);
        //    var nuevoJSON = JsonSerializer.Serialize(data);
        //    Assert.That(antiguoJSON, Is.EqualTo(nuevoJSON));

        //}
        #endregion

        #region GetByParamsTest
        /// <summary>
        /// NOTA: ESTOS TEST FALLAN YA QUE SQLITE NO SOPORTA PROYECCIONES DE PROYECCIÓNES
        /// </summary>
        /// <returns></returns>
        //[Test]
        //public async Task GetByparams_Test()
        //{
        //    var list = new List<MetaDTO>()
        //    {
        //        new MetaDTO()
        //        {
        //            MetaId = 0,
        //            NombreMeta = "Meta 1",
        //            Actividades = null,
        //            DetalleMeta= "Detalle por defecto",
        //            VigenciaID=1
        //        },
        //         new MetaDTO
        //         {
        //            MetaId = 0,
        //            NombreMeta = "Meta 2",
        //            Actividades = null,
        //            DetalleMeta= "Detalle por defecto",
        //            VigenciaID=1
        //        },
        //         new MetaDTO
        //         {
        //            MetaId = 0,
        //            NombreMeta = "Meta 3",
        //            Actividades = null,
        //            DetalleMeta= "Detalle por defecto",
        //            VigenciaID=2
        //        }
        //    };
        //    await _service.CreateOrUpdateRangeAsync(list);

        //    var data = await _service.GetByParamsAsync(new SIVIGILA.Commons.DTOs.Search.SearchMetaDTO());
        //    Assert.That(data.Total, Is.EqualTo(list.Count()));
        //}
        #endregion

        #region UpdateAsyncTest
        [Test]
        public void UpdateAsync_Not_Existing_ID()
        {

            var dto = new MetaDTO
            {
                MetaId = 5,
                NombreMeta = "Meta 3",
                Actividades = null,
                DetalleMeta = "Detalle por defecto",
                VigenciaID = 2
            };
            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.UpdateAsync(dto));
        }
        //ESTE TEST FALLA YA QUE SQLITE NO SOPORTA ACTUALIZAICÓN EN CASCADA DE ELEMENTOS
        //[Test]
        //public async Task UpdateAsync_Existing_ID()
        //{
        //    var dto = new MetaDTO
        //    {
        //        MetaId = 0,
        //        NombreMeta = "Meta 3",
        //        Actividades = null,
        //        DetalleMeta = "Detalle por defecto",
        //        VigenciaID = 2
        //    };
        //    await _service.AddAsync(dto);

        //    //Liberamos el Tracking de la entidad
        //    var undetachedEntriesCopy = _context.ChangeTracker.Entries()
        //        .Where(e => e.State != EntityState.Detached)
        //        .ToList();

        //    foreach (var entry in undetachedEntriesCopy)
        //        entry.State = EntityState.Detached;

        //    dto.MetaId = 1;
        //    dto.NombreMeta = "Meta 3.1";
        //    await _service.UpdateAsync(dto);
        //    var data = await _service.GetByIdAsync(1);

        //    Assert.That(data.NombreMeta, Is.EqualTo("Meta 3.1"));
        //}
        #endregion
    }
}
