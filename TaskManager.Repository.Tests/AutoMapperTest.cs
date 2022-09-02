using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;
using Xunit;

namespace TaskManager.Repository.Tests
{
    public class AutoMapperTest
    {
        private IMapper _mapper;

        public AutoMapperTest()
        {
            _mapper = AutoMapperFactory.BuildAutoMapper();
        }

        [Fact]
        public async System.Threading.Tasks.Task ShouldMapFromClientToClientDtoWithSuccess()
        {
            var clientDto = new ClientDto();
            var client = new Client()
            {
                Id = 1,
                Name = "Generali",
                Sector = "Assicurazioni"
            };

            clientDto = _mapper.Map<ClientDto>(client);

            Assert.NotNull(clientDto);
        }

    }
}
