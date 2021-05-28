using System;

namespace ApiLives.Core.Domain.Entities.ApiLives
{
    public class Live: LivesRepository.Data.IEntity
    {
        public Live() { }

        public Live(int id,string liveName, string channelName, DateTime liveDate, string liveLink, DateTime registrationDate,string livetime, bool statusLive)
        {
            this.Id = id;
            this.liveName = liveName;
            this.channelName = channelName;
            this.liveDate = liveDate;
            this.liveLink = liveLink;
            this.registrationDate = registrationDate;
            this.liveTime = livetime;
            this.statusLive = statusLive;
        }

     
        public String liveName { get; set; }
        public String channelName { get; set; }
        public DateTime liveDate { get; set; }
        public String liveTime { get; set; }
        public String liveLink { get; set; }
        public DateTime registrationDate { get; set; }
        public int Id { get; set; }
        public bool statusLive { get; set; }
    }
}
