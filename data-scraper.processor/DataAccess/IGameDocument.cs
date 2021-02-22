using System;

namespace DataScraper.DataAccess
{
	public interface IGameDocument : IDocument
	{
		string UID { get; set; }	
	}	
}
