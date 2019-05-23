using System;
using System.Collections;
using System.Collections.Generic;

public interface IVsnSaveHandler{
	
	void Save (Dictionary<string, string> dictionary, int saveSlot, Action<bool> callback);

	void Load(Dictionary<string, string> dictionary, int saveSlot, Action<Dictionary<string,string>> callback);

}

