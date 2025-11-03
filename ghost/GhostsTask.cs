using System;
using System.Text;

namespace hashes;

public class GhostsTask : 
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
	IMagic
{
	static byte[] forDoc = new byte[] {1};
	Vector vector = new Vector(1, 1);
	Segment segment = new Segment(new Vector(1, 1), new Vector(2, 2));
	Document document = new Document("Title", Encoding.UTF8, forDoc);
	Cat cat = new Cat("Aza", "Sphinx", DateTime.MaxValue);
	Robot robot = new Robot("123");
	
	
	public void DoMagic()
	{
		vector.Add(new Vector(1, -1));
		segment.Start.Add(new Vector(1,0));
		forDoc[0]++;
		cat.Rename("Awem Awep");
		Robot.BatteryCapacity++;
	}

	Vector IFactory<Vector>.Create() => vector;
	Segment IFactory<Segment>.Create() => segment;
	Document IFactory<Document>.Create() => document;
	Cat IFactory<Cat>.Create() => cat;
	Robot IFactory<Robot>.Create() => robot;
}