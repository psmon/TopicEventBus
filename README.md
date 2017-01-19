# TopicEventBus
http://doc.akka.io/docs/akka/snapshot/java/event-bus.htmlLet's implement Event Bus in C #, a Pub / Sub module included in Java Akka.

The Pub / Sub model has the advantage of easily separating and processing messages with the concept of a channel (topic).

It is easy to assign a channel function to a simple chat server.

For example, I would like to receive news from a news provider,

If you want to, a news provider is a feature that allows you to spread real-time news to only the users you want, by the topic / language they serve.



//Create Act Act System for Testing
ActorSystem system = TopicEventBus.inItSystem("TestTopic"); //ActorSystem Init
 
//Create user
TopicEventBus.CreateActor("A");
TopicEventBus.CreateActor("B");
TopicEventBus.CreateActor("C");
 
//A user wants to receive News A in English
TopicEventBus.Subscribe("A", "newsA", "en").Wait();
 
//User B wants to receive news A in Korean
TopicEventBus.Subscribe("B", "newsA", "kr").Wait();
 
//Users of c want to receive News B in English
TopicEventBus.Subscribe("C", "newsB", "en").Wait();
 
 
//Server real-time messages (server sprays news regardless of user)
TopicEventBus.Publish("newsA", "Hi...here news A", "en");
TopicEventBus.Publish("newsA", "여기에 새로운 뉴스가 있습니다.", "kr");
TopicEventBus.Publish("newsB", "Hi...here news B", "en");

Result:
create actor name:A
create actor name:B
create actor name:C
Subscribe actor name:A to newsA
Subscribe actor name:B to newsA
A FirstSet LangCode en from newsA
B FirstSet LangCode kr from newsA
Subscribe actor name:C to newsB
C FirstSet LangCode en from newsB
Publish Topic:newsA msg:Hi...here news A
Publish Topic:newsA msg:여기에 새로운 뉴스가 있습니다.
A가 뉴스를 받음 Hi...here news A from newsA
Publish Topic:newsB msg:Hi...here news B
C가 뉴스를 받음 Hi...here news B from newsB
B가 뉴스를 받음 여기에 새로운 뉴스가 있습니다. from newsA
