using Actors.AkkaNET.App.Actors;
using Actors.AkkaNET.App.Messages;
using Xunit;
using Akka.TestKit.Xunit2;
using FluentAssert;

namespace Actors.AkkaNET.App.Tests
{
    public class Tests: TestKit
    {
        [Fact]
        public void Device_actor_must_reply_with_empty_reading_if_no_temperature_is_known()
        {
            var probe = CreateTestProbe();
            var deviceActor = Sys.ActorOf(Device.Props("group", "device"));

            deviceActor.Tell(new ReadTemperature(requestId: 42), probe.Ref);
            RespondTemperature response = probe.ExpectMsg<RespondTemperature>();
            response.RequestId.ShouldBeEqualTo(42);
            response.Value.ShouldBeNull();
        }

        [Fact]
        public void Device_actor_must_reply_with_latest_temperature_reading()
        {
            var probe = CreateTestProbe();
            var deviceActor = Sys.ActorOf(Device.Props("group", "device"));

            deviceActor.Tell(new RecordTemperature(requestId: 1, value: 24.0), probe.Ref);
            probe.ExpectMsg<TemperatureRecorded>(s => s.RequestId == 1);

            deviceActor.Tell(new ReadTemperature(requestId: 2), probe.Ref);
            var response1 = probe.ExpectMsg<RespondTemperature>();
            response1.RequestId.ShouldBeEqualTo(2);
            response1.Value.ShouldBeEqualTo(24.0);

            deviceActor.Tell(new RecordTemperature(requestId: 3, value: 55.0), probe.Ref);
            probe.ExpectMsg<TemperatureRecorded>(s => s.RequestId == 3);

            deviceActor.Tell(new ReadTemperature(requestId: 4), probe.Ref);
            var response2 = probe.ExpectMsg<RespondTemperature>();
            response2.RequestId.ShouldBeEqualTo(4);
            response2.Value.ShouldBeEqualTo(55.0);
        }
    }
}
