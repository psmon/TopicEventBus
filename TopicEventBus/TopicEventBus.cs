using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Akka;
using Akka.Actor;


namespace TopicEventBus
{
    public class SubChannel
    {
        public string topic;
        public string lang;

        public SubChannel(string _topic,string _lang)
        {
            topic = _topic;
            lang = _lang;
        }
    }

    public class ChannelData
    {
        public string topic;
        public string lang;
        public string msg;
        public ChannelData(string _topic,string _msg,string _lang)
        {
            topic = _topic;
            msg = _msg;
            lang = _lang;
        }
    }


    public class TopicEventBus
    {
        static int actorTimeOut = 500;  //Milliseconds
        static ActorSystem system = null;
        static public ActorSystem inItSystem(string systemName)
        {
            if (system == null)
                system = ActorSystem.Create(systemName);

            return system;
        }

        static public async void CreateActor(string uuid)
        {
            //IActorRef subscriber = await system.ActorSelection(uuid).ResolveOne(TimeSpan.FromMilliseconds(actorTimeOut));
            try
            {
                var subscriber = system.ActorOf<SomeActor>(uuid);
                //subscriber.Tell(sockHandler);
                Console.WriteLine("create actor name:" + subscriber.Path.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine("CreateActor::" + e.Message);
            }

        }

        static public async Task<bool> Subscribe(string uuid,string topic, string lang)
        {
            IActorRef subscriber = await system.ActorSelection("user/" + uuid).ResolveOne(TimeSpan.FromMilliseconds(actorTimeOut));            
            SubChannel subChannel = new SubChannel(topic, lang);            
            subscriber.Tell(subChannel);
            Console.WriteLine("Subscribe actor name:" + subscriber.Path.Name + " to " + topic);
            system.EventStream.Subscribe(subscriber, typeof(ChannelData));            
            return true;

        }

        static public async void Publish(string topic, string msg,string lang)
        {
            ChannelData pubData = new ChannelData(topic, msg, lang);
            Console.WriteLine("Publish Topic:" + pubData.topic + " msg:" + pubData.msg);            
            system.EventStream.Publish(pubData);            
        }

        static public async Task<bool> DeletedActor(string uuid)
        {
            bool isDeleted = false;
            try
            {
                IActorRef subscriber = await system.ActorSelection("user/" + uuid).ResolveOne(TimeSpan.FromMilliseconds(actorTimeOut));
                Console.WriteLine("Deleted actor name:" + subscriber.Path.Name);
                isDeleted = await subscriber.GracefulStop(TimeSpan.FromMilliseconds(actorTimeOut));
            }
            catch (Exception e)
            {
                Console.WriteLine("DeletedActor:" + e.Message);
            }

            return isDeleted;
        }

    }
}
