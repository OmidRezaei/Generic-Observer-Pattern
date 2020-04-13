# Generic Observer Pattern
 
A punch of simple classes to implement variables that use observer pattern.

They Include:
	Generic Classes
		Subject
		Complarable Subject
		Subject Struct
		Complarable Subject Struct
	Built-in
		int
		float
		double
		boolean
		etc
They are all inhertiable.

It comes with a unity edtior code and some built-in unity types.
If you wish to use only the C# codes, you can delete the unity folder.

Observers must implement IObserver interface.

Casting while getting is supported.

Campareing is supported for ComparableSubject class and struct and built-in types.

For reference types, modifing value's properties will NOT call NotifyObservers() method. Use Modify() instead if you wish it to do so or call the said method manually afterwards.
