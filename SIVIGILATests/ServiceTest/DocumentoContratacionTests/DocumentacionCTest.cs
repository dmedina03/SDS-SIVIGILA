using FluentValidation;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Models.Context;
using SIVIGILA.Repository;
using SIVIGILA.Service.LineaService.Validation;
using SIVIGILA.Service.LineaService;
using SIVIGILATests.AdminSQLITE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIVIGILA.Service.DocumentoContServices;
using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Service.DocumentoContServices.Validation;
using SIVIGILA.Commons.ErrorHandling.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using SIVIGILA.Models.Entities;
using System.Text.Json;

namespace SIVIGILATests.ServiceTest.DocumentoContratacionTest
{
    [TestFixture]
    public class DocumentacionCTest
    {
        private IDocumentoContService  _service;
        private IValidator<DocumentoCDTO> _validator;
        private ConnectionFactory factory = new();
        private context _context;

        [SetUp]
        public void SetUp()
        {
            if (_context != null)
                _context.Dispose();
            //Get the instance of BlogDBContext
            _context = factory.CreateContextForSQLite();
            var repository = new DocumentoContRepository(_context);
            _validator = new DocumentoContValidator(repository);
            _service = new DocumentoContService(repository, _validator);
        }

        #region AddAsyncTests
        [Test]
        public void AddAsync_With_No_Zero_ID_TEST()
        {
            var dto = new DocumentoCDTO()
            {
                DocumentoConID = 1,
                NombreDocumento = "Documento 1",
                Estado = true
            };
            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.AddAsync(dto));

        }
        [Test]
        public async Task AddAsync_With_No_Valid_Name_TEST()
        {
            var dto1 = new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            };
            await _service.AddAsync(dto1);
            var dto2 = new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1  ",
                Estado = true
            };
            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.AddAsync(dto2));
        }
        [Test]
        public async Task AddAsync_ValidValues_TEST()
        {
            var dto = new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            };
            int data = await _service.AddAsync(dto);
            Assert.AreEqual(data, 1);
        }
        #endregion
        #region CreateOrUpdateRangeTests
        [Test]
        public async Task CreateOrUpdateRange_Duplicades_IDS_Test()
        {
            var data = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    DocumentoConID=1,
                    NombreDocumento="Documento 1",
                    Estado= true
                },
                 new DocumentoCDTO
                 {
                    DocumentoConID=1,
                    NombreDocumento="Documento 1",
                    Estado= true
                }
            };
            await _service.AddAsync(new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            });

            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.CreateOrUpdateRangeAsync(data));
        }

        [Test]
        public async Task CreateOrUpdateRange_Duplicades_Names_Test()
        {
            var data = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    DocumentoConID=0,
                    NombreDocumento="Documento 2",
                    Estado= true
                },
                 new DocumentoCDTO
                 {
                    DocumentoConID=0,
                    NombreDocumento="Documento 2",
                    Estado= true
                }
            };
            await _service.AddAsync(new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            });

            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.CreateOrUpdateRangeAsync(data));
        }

        [Test]
        public async Task CreateOrUpdateRange_With_Alredy_Inserted_Name_Test()
        {
            var data = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    DocumentoConID=0,
                    NombreDocumento="Documento 1",
                    Estado= true
                },
                 new DocumentoCDTO
                 {
                    DocumentoConID=0,
                    NombreDocumento="Documento 2",
                    Estado= true
                }
            };
            await _service.AddAsync(new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            });

            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.CreateOrUpdateRangeAsync(data));
        }

        [Test]
        public async Task CreateOrUpdateRange_Valid_Test()
        {
            var data = new List<DocumentoCDTO>()
            {
                 new DocumentoCDTO
                 {
                    DocumentoConID=0,
                    NombreDocumento="Documento 2",
                    Estado= true
                },
                 new DocumentoCDTO
                 {
                    DocumentoConID=0,
                    NombreDocumento="Documento 3",
                    Estado= true
                },
            };
            var a = new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            };

            await _service.AddAsync(a);

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
            var list = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 1",
                    Estado=true
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 2",
                    Estado=true
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 3",
                    Estado=true
                },

            };
            await _service.CreateOrUpdateRangeAsync(list);

            bool data = await _service.ExistByIDAsync(id);
            Assert.That(data, Is.EqualTo(EspectedResponse));
        }

        [Test]
        [TestCase("Documento 1", true)]
        [TestCase("Documento 2", true)]
        [TestCase("Documento 3", true)]
        [TestCase("Documento 4", false)]
        [TestCase("Documento 5", false)]
        public async Task ExistByNameAsync_Tests(string name, bool EspectedResponse)
        {
            var list = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 1",
                    Estado=true
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 2",
                    Estado=true
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 3",
                    Estado=true
                },

            };
            await _service.CreateOrUpdateRangeAsync(list);

            bool data = await _service.ExistByNameAsync(name);
            Assert.That(data, Is.EqualTo(EspectedResponse));
        }
        #endregion

        #region GetAllTests
        [Test]
        public async Task GetAllAsync_Test()
        {
            var list = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 1",
                    Estado=false
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 2",
                    Estado=false
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 3",
                    Estado=true
                },

            };
            await _service.CreateOrUpdateRangeAsync(list);

            var data = await _service.GetAllAsync();
            Assert.That(data.Count(), Is.EqualTo(1));

        }
        #endregion

        #region GetByIDAsyncTests
        [Test]
        public async Task GetByIDAsync_No_Existing_ID_Test()
        {
            var dto = new DocumentoCDTO()
            {
                NombreDocumento = "Documento 1",
                Estado = true
            };
            await _service.AddAsync(dto);   //Se inserta con el cero

            Assert.ThrowsAsync<NotFoundException>(async () => await _service.GetByIdAsync(50));

        }
        [Test]
        public async Task GetByIDAsync_Existing_ID_Test()
        {
            var dto = new DocumentoCDTO()
            {
                NombreDocumento = "Documento 1",
                Estado = true
            };
            await _service.AddAsync(dto);   //Se inserta con el cero

            var data = await _service.GetByIdAsync(1);
            //Validamos que sean iguales ambos objetos a traves de la serialziación
            dto.DocumentoConID = 1;
            var antiguoJSON = JsonSerializer.Serialize(dto);
            var nuevoJSON = JsonSerializer.Serialize(data);
            Assert.That(antiguoJSON, Is.EqualTo(nuevoJSON));

        }
        #endregion

        #region GetByParamsTest
        [Test]
        public async Task GetByparams_Test()
        {
            var list = new List<DocumentoCDTO>()
            {
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 1",
                    Estado=false
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 2",
                    Estado=false
                },
                new DocumentoCDTO()
                {
                    NombreDocumento="Documento 3",
                    Estado=true
                },

            };
            await _service.CreateOrUpdateRangeAsync(list);

            var data = await _service.GetByParamsAsync(new SIVIGILA.Commons.DTOs.Search.SearchDocumentoDTO());
            Assert.That(data.Total, Is.EqualTo(3));
        }
        #endregion

        #region UpdateAsyncTest
        [Test]
        public void UpdateAsync_Not_Existing_ID()
        {

            var dto = new DocumentoCDTO()
            {
                DocumentoConID = 1,
                NombreDocumento = "Documento 1",
                Estado = false
            };
            Assert.ThrowsAsync<ValidationDataException>(async () => await _service.UpdateAsync(dto));
        }
        [Test]
        public async Task UpdateAsync_Existing_ID()
        {
            var dto = new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = false
            };
            await _service.AddAsync(dto);

            //Liberamos el Tracking de la entidad
            var undetachedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in undetachedEntriesCopy)
                entry.State = EntityState.Detached;

            dto.DocumentoConID = 1;
            dto.Estado = true;
            await _service.UpdateAsync(dto);
            var data = await _service.GetByIdAsync(1);

            Assert.That(data.Estado, Is.EqualTo(true));
        }
        #endregion

        #region UpdateStateTests
        [Test]
        public void UpdateStateAsync_Not_Existing_ID()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _service.UpdateStateAsync(5, true));
        }
        [Test]
        public async Task UpdateStateAsync_Existing_ID()
        {
            var dto = new DocumentoCDTO()
            {
                DocumentoConID = 0,
                NombreDocumento = "Documento 1",
                Estado = true
            };
            int data = await _service.AddAsync(dto);
            //Liberamos el Tracking de la entidad
            var undetachedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached)
                .ToList();

            foreach (var entry in undetachedEntriesCopy)
                entry.State = EntityState.Detached;

            bool response = await _service.UpdateStateAsync(1, false);
            var dataDTO = await _service.GetByIdAsync(1);
            Assert.That(dataDTO.Estado, Is.EqualTo(false));
        }
        #endregion
    }

}
