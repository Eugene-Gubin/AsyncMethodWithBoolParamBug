using System;
using System.Drawing;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace AsyncMethodWithBoolParam
{
	public partial class AsyncMethodWithBoolParamViewController : UIViewController
	{
		async void ButtonHandler(object sender, EventArgs e)
		{
			await CallAsync (true);
//			await CallAsync2 (true);
//			await CallAsync3 (true);
//			await CallAsync4 (true);
//			await CallAsync5 (1);
		}

		#region Case 1
		// May work but not from first time.
		// Work if boolParam not passed to DoSomething (constant used instead).
		async Task CallAsync(bool boolParam)
		{
			await Task.Delay (10);
			DoSomething (boolParam);
		}

		void DoSomething(bool b)
		{
			if (b)
				btnRun.SetTitleColor (UIColor.Red, UIControlState.Normal);  
		}
		#endregion

		#region Case 2
		// Not work at all.
		async Task CallAsync2(bool boolParam)
		{
			await DoSomething2 (boolParam);
		}

		async Task DoSomething2(bool b)
		{
			await Task.Delay (10);
			if (b)
				btnRun.SetTitleColor (UIColor.Red, UIControlState.Normal); 
		}
		#endregion

		#region Case 3
		// Same as Case 1 but uses nullable. It works.
		async Task CallAsync3(bool? boolParam)
		{
			await Task.Delay (10);
			DoSomething3 (boolParam.Value);
		}

		void DoSomething3(bool b)
		{
			if (b)
				btnRun.SetTitleColor (UIColor.Red, UIControlState.Normal);  
		}
		#endregion

		#region Case 4
		// Same as Case 2 but uses nullable. It works.
		async Task CallAsync4(bool? boolParam)
		{
			await DoSomething4 (boolParam);
		}

		async Task DoSomething4(bool? b)
		{
			await Task.Delay (10);
			if (b.Value)
				btnRun.SetTitleColor (UIColor.Red, UIControlState.Normal); 
		}
		#endregion

		#region Case 5
		// Uses value type but not bool. It works.
		async Task CallAsync5(int intParam)
		{
			await Task.Delay (10);
			DoSomething5 (intParam != 0);
		}

		void DoSomething5(bool b)
		{
			if (b)
				btnRun.SetTitleColor (UIColor.Red, UIControlState.Normal);  
		}
		#endregion

		public AsyncMethodWithBoolParamViewController () : base ("AsyncMethodWithBoolParamViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			btnRun.TouchUpInside += ButtonHandler;
		}
	}
}

