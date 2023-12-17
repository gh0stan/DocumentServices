using AbonentService.AsyncDataServices;
using AbonentService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbonentService.Api.Tests.TestDoubles
{
    public class SpyMessageBusClient : IMessageBusClient
    {
        public int NumberOfCalls { get; private set; }
        public string EventName { get; private set; }

        public void CreateNewAbonent(AbonentCreatedDto abonentCreatedDto)
        {
            NumberOfCalls++;
            EventName = abonentCreatedDto.Event;
            return;
        }
    }
}
