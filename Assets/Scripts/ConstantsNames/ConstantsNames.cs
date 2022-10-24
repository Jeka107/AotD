using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstantsNames
{
	#region Constants - Tags
	public class ObjectTag
	{
		public const string Door = "Door";
		public const string Safe = "Safe";
		public const string Collectable = "Collectable";
		public const string Friend = "Friend";
		public const string Null = "Untagged";
		public const string Switch = "Switch";
		public const string WallCut = "WallCut";
		public const string Candle = "Candle";
		public const string ExitDoor = "MainExit";
		public const string ItemPed = "ItemPed";
		public const string FinalButton = "FinalButton";
	}
	#endregion

	#region Constans-Id
	public class ObjectId
    {
		public const string InventoryKey = "Inventory_Key";
		public const string InventoryKnife = "Inventory_Knife";
		public const string InventoryNote = "Inventory_Note";
		public const string InventoryMirror = "Inventory_Mirror";
		public const string InventorySyringe = "Inventory_Syringe";
	}
	#endregion

	#region Constans-Notebook Item Types
	public class NotebookItemTypes
    {
		public const string Note = "Notebook_Note";
		public const string Tape = "Notebook_Tape";
	}
	#endregion
}
