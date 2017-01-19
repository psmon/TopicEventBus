using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Akka;
using Akka.Actor;

namespace TopicEventBus
{
    public class SomeActor : ReceiveActor
    {
        SubChannel subChannel = null;

        bool isFillter(object _msg)
        {
            bool result = true;
            ChannelData msg = _msg as ChannelData;
            if (msg.topic == subChannel.topic && msg.lang == subChannel.lang)
                result = false;
            return result;
        }

        public SomeActor()
        {

            Receive<SubChannel>(data =>
            {
                if (subChannel == null)
                    Console.WriteLine("{0} FirstSet LangCode {1} from {2}", Self.Path.Name, data.lang, data.topic);
                else
                    Console.WriteLine("{0} Changed LangCode {1} to {2} from {3}", Self.Path.Name, subChannel.lang, data.lang, data.topic);

                subChannel = data;
            });

            Receive<ChannelData>(data =>
            {
                if (isFillter(data))
                    return;
                
                Console.WriteLine("{0}가 뉴스를 받음 {1} from {2}", Self.Path.Name, data.msg , data.topic);

            });

        }

    }
}
