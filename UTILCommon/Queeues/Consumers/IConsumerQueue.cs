using System.Threading.Tasks;
using UTILCommon.Queeues.Core;

namespace UTILCommon.Queeues.Consumers {

    public interface IConsumerQueue {

        /// <summary>
        /// 
        /// </summary>
        Task receiveFromQueue();

        /// <summary>
        /// 
        /// </summary>
        Task receiveFromQueue(MqConfiguration MqConfiguration);
    }

}