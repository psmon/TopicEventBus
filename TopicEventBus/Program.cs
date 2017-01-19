using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka;
using Akka.Actor;

namespace TopicEventBus
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = TopicEventBus.inItSystem("TestTopic"); //ActorSystem Init

            Task.Delay(1000).Wait();

            //사용자 생성..
            TopicEventBus.CreateActor("A");
            TopicEventBus.CreateActor("B");
            TopicEventBus.CreateActor("C");

            //A의 사용자는 뉴스A를 영어로 받기를 원함
            TopicEventBus.Subscribe("A", "newsA", "en").Wait();

            //B의 사용자는 뉴스A를 한글로 받기를 원함
            TopicEventBus.Subscribe("B", "newsA", "kr").Wait();

            //c의 사용자는 뉴스B를 영어로 받기를 원함
            TopicEventBus.Subscribe("C", "newsB", "en").Wait();


            //서버 실시간 메시지 ( 서버는 사용자에 상관없이 뉴스를 뿌린다.)
            TopicEventBus.Publish("newsA", "Hi...here news A", "en");
            TopicEventBus.Publish("newsA", "여기에 새로운 뉴스가 있습니다.", "kr");
            TopicEventBus.Publish("newsB", "Hi...here news B", "en");

            Task.Delay(1000).Wait();            
        }
    }
}
