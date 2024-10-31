using ApiNet.Controllers;
using ApiNet.DTOs;
using ApiNet.Exceptions;
using ApiNet.Model;
using ApiNet.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestApi
{
    public class ControllerEquiposTest
    {

        private readonly Mock<IServiceEquipo> _mockService;
        private readonly ApiController _controller;

        public ControllerEquiposTest()
        {
            // Inicializamos el Mock del servicio
            _mockService = new Mock<IServiceEquipo>();

            // Creamos el controlador, pasándole el mock del servicio
            _controller = new ApiController(_mockService.Object);
        }

        [Fact]
        public async Task Nuevo_AgregaEquipoDevuelveOk()
        {
            // Arrange
            var equipoDto = new EquipoNuevoDTO { /* Asigna propiedades si es necesario */ };

            _mockService.Setup(service => service.AgregaEquipo(equipoDto))
                        .Returns(Task.CompletedTask);

            var controller = new ApiController(_mockService.Object);

            // Act
            var result = await controller.Nuevo(equipoDto);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockService.Verify(service => service.AgregaEquipo(equipoDto), Times.Once);
        }


        [Fact]
        public async Task GetAll_DevuelveListaDeEquipos()
        {
            // Arrange
            var equiposMock = new List<EquipoRespuestaDTO>
            {
                new EquipoRespuestaDTO {
                    Id = 1,
                    Nombre = "TV",
                    Descripcion = "Tv smart 32"
                },
                new EquipoRespuestaDTO { 
                    Id =2,
                    Nombre = "CELULAR",
                    Descripcion = "Celu que no sirve"
                }
            };

            _mockService.Setup(service => service.GetEquipoList())
                        .ReturnsAsync(equiposMock);

            var controller = new ApiController(_mockService.Object);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var equipos = Assert.IsType<List<EquipoRespuestaDTO>>(okResult.Value);
            Assert.Equal(2, equipos.Count);
        }

        [Fact]
        public async Task RemoveEquipo_ReturnsOkResult_WhenEquipoExists()
        {
            // Arrange
            var equipoId = 5;

            // Configuramos el mock para que no haga nada cuando se llame a Delete
            _mockService.Setup(service => service.Delete(equipoId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Borra(equipoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Equipo con id {equipoId} eliminado!!!", okResult.Value);

            // Verifica que se haya llamado a Delete con el ID correcto
            _mockService.Verify(service => service.Delete(equipoId), Times.Once);
        }

        [Fact]
        public async Task GetEquipoById_ReturnsOkResult_WhenEquipoExists()
        {
            // Arrange
            var equiposMock = new List<EquipoRespuestaDTO>
                {
                    new EquipoRespuestaDTO {
                        Id = 1,
                        Nombre = "TV",
                        Descripcion = "Tv smart 32"
                    },
                    new EquipoRespuestaDTO {
                        Id = 2,
                        Nombre = "CELULAR",
                        Descripcion = "Celu que no sirve"
                    }
                };

            // Configuramos el mock para que devuelva el equipo cuando se llame a GetById con el ID 1
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(equiposMock.First(eq => eq.Id == 1));

            var controller = new ApiController(_mockService.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var equipo = Assert.IsType<EquipoRespuestaDTO>(okResult.Value);
            Assert.Equal("TV", equipo.Nombre);
        }


        [Fact]
        public async Task UpdateEquipo_ReturnsOkResult_WhenEquipoExists()
        {
            // Arrange
            var existingEquipo = new EquipoRespuestaDTO
            {
                Id = 1,
                Nombre = "TV",
                Descripcion = "Tv smart 32"
            };

            var updatedEquipo = new EquipoNuevoDTO
            {
                Nombre = "Cel",
                Descripcion = "Celular"
            };

            // Configuramos el mock para que devuelva el equipo existente al buscar por ID
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(existingEquipo);

            // Configuramos el mock para que no arroje excepciones y simule una actualización exitosa
            _mockService.Setup(service => service.Update(updatedEquipo, 1)).Returns(Task.CompletedTask);

            var controller = new ApiController(_mockService.Object);

            // Act
            var result = await controller.UpdateEquipo(1, updatedEquipo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal($"Equipo con id {1} actualizado", okResult.Value); // Verificamos el mensaje de éxito
        }


        [Fact]
        public async Task UpdateEquipo_ReturnsBadRequest_WhenNombreAlreadyExists()
        {
            // Arrange
            var existingEquipo = new EquipoRespuestaDTO
            {
                Id = 1,
                Nombre = "TV",
                Descripcion = "Tv smart 32"
            };

            var updatedEquipo = new EquipoNuevoDTO
            {
                Nombre = "TV", // El nombre ya existe
                Descripcion = "Celular"
            };

            // Configuramos el mock para que devuelva el equipo existente
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(existingEquipo);

            // Simulamos que el nombre ya está en uso
            _mockService.Setup(service => service.Update(updatedEquipo, 1)).ThrowsAsync(new EquipoExiste(updatedEquipo.Nombre));

            var controller = new ApiController(_mockService.Object);

            // Act
            var result = await controller.UpdateEquipo(1, updatedEquipo);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal($"Ya existe un equipo con el nombre '{updatedEquipo.Nombre}'.", badRequestResult.Value);
        }

        
    }
}
