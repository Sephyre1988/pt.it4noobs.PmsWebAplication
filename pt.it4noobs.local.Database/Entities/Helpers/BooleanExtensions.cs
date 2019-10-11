namespace pt.it4noobs.local.Database.Entities.Helpers
{
	public static class BooleanExtensions
	{
		public static string ToBooleanString(this bool value)
		{
			return value.ToString().ToLower();
		}

		public static string ToPortugueseYesNoIntialString(this bool value)
		{
			return value ? "S" : "N";
		}

		public static bool ToBoolFromPortugueseYesNoIntialString(this string value)
		{
			return (value == "S");
		}

		public static int ToBooleanInt(this bool value)
		{
			return value ? 1 : 0;
		}
	}
}