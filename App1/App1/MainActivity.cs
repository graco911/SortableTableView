using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CodeCrafters.TableViews;
using CodeCrafters.TableViews.Toolkit;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public SortableTableView tableview { get; private set; }
        public SimpleTableHeaderAdapter simpleTableHeaderAdapter { get; private set; }
        public int rowColorEven { get; private set; }
        public int rowColorOdd { get; private set; }
        private readonly Database database = new Database();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            tableview = FindViewById<SortableTableView>(Resource.Id.tableView);

            simpleTableHeaderAdapter = new SimpleTableHeaderAdapter(this, "Prueba 1", "Prueb 2", "Prueba 3", "Prueba 4");
            simpleTableHeaderAdapter.SetTextColor(ContextCompat.GetColor(this, Resource.Color.table_header_text));
            tableview.HeaderAdapter = simpleTableHeaderAdapter;

            rowColorEven = ContextCompat.GetColor(this, Resource.Color.table_data_row_even);
            rowColorOdd = ContextCompat.GetColor(this, Resource.Color.table_data_row_odd);
            tableview.SetDataRowBackgroundProvider(TableDataRowBackgroundProviders.AlternatingRowColors(rowColorEven, rowColorOdd));
            tableview.HeaderSortStateViewProvider = SortStateViewProviders.BrightArrows();

            tableview.SetColumnWeight(0, 2);
            tableview.SetColumnWeight(1, 3);
            tableview.SetColumnWeight(2, 3);
            tableview.SetColumnWeight(3, 2);

            tableview.SetColumnComparator(0, database.GetCarProducerComparator());
            tableview.SetColumnComparator(1, database.GetCarNameComparator());
            tableview.SetColumnComparator(2, database.GetCarPowerComparator());
            tableview.SetColumnComparator(3, database.GetCarPriceComparator());
            tableview.DataAdapter = new CarTableDataAdapter(this, database.Cars);
            tableview.DataClick += (sender, e) =>
            {
                var car = (Car)e.ClickedData;
                var carString = car.Producer.Name + " " + car.Name;
                Toast.MakeText(this, carString, ToastLength.Short).Show();
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

