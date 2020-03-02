using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText incomePerHourEditText;
        EditText workHourPerDayEditText;
        EditText taxRateEditText;
        EditText savingRateEditText;

        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView annualSavingsTextView;
        TextView spendableIncomeTextView;

        Button calculateButton;
        RelativeLayout resultLayout;

        bool inputCalculated = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        void ConnectViews()
        {
            //Get references
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHourPerDayEditText = FindViewById<EditText>(Resource.Id.workHoursPerDayEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            savingRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);

            workSummaryTextView = FindViewById<TextView>(Resource.Id.workSummaryTextView);
            grossIncomeTextView = FindViewById<TextView>(Resource.Id.grossIncomeTextView);
            taxPayableTextView = FindViewById<TextView>(Resource.Id.taxTextView);
            annualSavingsTextView = FindViewById<TextView>(Resource.Id.annualSavingsTextView);
            spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spendableTextView);

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;

        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if (inputCalculated)
            {
                inputCalculated = false;
                calculateButton.Text = "Calculate";
                ClearInput();
                return;
            }
            //Take Inputs from user
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHourPerDay = double.Parse(workHourPerDayEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);
            double savingsRate = double.Parse(savingRateEditText.Text);

            //Calculate annual income, tax and savings
            double annualWorkHourSummary = workHourPerDay * 5 * 50; //There are 52 weeks in a year, lets assume the user will take a two week off
            double annualIncom = incomePerHour * workHourPerDay * 5 * 50;
            double taxPayable = (taxRate / 100) * annualIncom; // divided by 100 because in app you enter amount of taxrate in %. so if taxrate is 20(entered by theuser) then 20/100 = 0.20 which is 20%
            double annualSavings = (savingsRate / 100) * annualIncom;
            double spendableIncom = annualIncom - annualSavings - taxPayable;

            //Display results of calculation
            grossIncomeTextView.Text = annualIncom.ToString("#,##") + " USD";
            workSummaryTextView.Text = annualWorkHourSummary.ToString("#,##") + " HRS";
            taxPayableTextView.Text = taxPayable.ToString("#,##") + " USD";
            annualSavingsTextView.Text = annualSavings.ToString("#,##") + " USD";
            spendableIncomeTextView.Text = spendableIncom.ToString("#,##") + " USD";

            resultLayout.Visibility = Android.Views.ViewStates.Visible;
            inputCalculated = true;
            calculateButton.Text = "Clear";
        }

        void ClearInput()
        {
            incomePerHourEditText.Text = "";
            workHourPerDayEditText.Text = "";
            taxRateEditText.Text = "";
            savingRateEditText.Text = "";

            resultLayout.Visibility = Android.Views.ViewStates.Invisible;
        }
    }
}