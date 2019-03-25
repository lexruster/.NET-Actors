using System.Threading.Tasks;

namespace Actors.Orlean.Grains
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
