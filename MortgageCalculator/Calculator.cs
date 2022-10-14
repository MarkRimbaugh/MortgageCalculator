using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator
{
    public class MortgageCalculator
    {
        public double MonthlyIncome { get; set; }
        public double MarketValue { get; set; }
        public double PurchasePrice { get; set; }
        public double HOA_Fee { get; set; }
        public double DownPayment { get; set; }
        public int PaymentsPerYear { get; set; }
        public double LoanAmount { set; get; }
        public double InterestRate { get; set; }
        public int Term { get; set; }

        const double TAXES_AND_CLOSING_CLOSTS = 2500;
        const double PROPERTY_TAX = .0125;
        const double HOMEOWNERS_INSURANCE = .0075;
        const double INCOME_THRESHOLD = .25;

        public MortgageCalculator() { }

        public MortgageCalculator(double monthlyIncome, double marketValue, double purchasePrice, double hoaFee, double downPayment, int paymentsPerYear, double interestRate, int term)
        {
            MonthlyIncome = monthlyIncome;
            MarketValue = marketValue;
            PaymentsPerYear = paymentsPerYear;
            HOA_Fee = hoaFee;
            DownPayment = downPayment;
            InterestRate = interestRate;
            Term = term;
            PurchasePrice = purchasePrice;
        }

        public double CalculateTotalLoanValue()
        {
            double loanAmount = (PurchasePrice - DownPayment);
            double originationFee = loanAmount * .01;
            double totalLoanValue = loanAmount + originationFee + TAXES_AND_CLOSING_CLOSTS;

            return Math.Round(totalLoanValue,2);
        }

        public double CalculateBaseLoanAmount()
        {
            return Math.Round(PurchasePrice - DownPayment);
        }

        public double CalculateBasePayment()
        {
            double monthlyPayment = ((InterestRate / PaymentsPerYear) * CalculateTotalLoanValue()) / (1 - Math.Pow(1 + (InterestRate / PaymentsPerYear), (Term * PaymentsPerYear) * -1));
             
            return Math.Round(monthlyPayment,2);
        }

        public double CalculateTotalMonthlyPayment()
        {
            double basePayment = CalculateBasePayment();
            double hoa = CalculateHOAPayment();
            double escrow = CalculateEscrowPayment();
            double insurance = 0;
            if (RequiresLoanInsurance())
            {
                insurance = CalculateLoanInsurancePayment();
            }

            return Math.Round((basePayment + insurance + hoa + escrow),2);
        }

        public double CalculateTotalLoanAmount()
        {
            double baseAmount = CalculateBaseLoanAmount();
            double origination = CalculateOriginationFee();

            return Math.Round((baseAmount + origination + TAXES_AND_CLOSING_CLOSTS),2);
        }

        public double CalculateOriginationFee()
        {
            return Math.Round(((PurchasePrice - DownPayment) * .01),2);
        }

        public double CalculateEquityValue()
        {
            double equity = MarketValue - (CalculateTotalLoanValue());
            return Math.Round(equity,2);
        }

        public double CalculateEquityPercentage()
        {
            double equityPercentage = Math.Round((CalculateEquityValue() / MarketValue) * 100, 2);
            return equityPercentage;
        }
        
        public bool RequiresLoanInsurance()
        {
            if (CalculateEquityValue() < (.1 * CalculateTotalLoanAmount()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double CalculateLoanInsurancePayment()
        {
            if (RequiresLoanInsurance())
            {
                return Math.Round(((CalculateTotalLoanValue() * .01) / PaymentsPerYear),2);
            }
            else
            {
                return 0;
            }
        }

        public double CalculateHOAPayment()
        {
            return Math.Round(HOA_Fee / 12,2);
        }


        public double CalculateEscrowPayment()
        {
            double homeownersInsurance = MarketValue * HOMEOWNERS_INSURANCE;
            double propertyTaxes = MarketValue * PROPERTY_TAX;
            double escrowPayment = (homeownersInsurance + propertyTaxes) / (12);

            return Math.Round(escrowPayment,2);
        }

        public string ApprovalRecommendation()
        {
            double incomeThreshold = MonthlyIncome * INCOME_THRESHOLD;
            

            if(CalculateTotalMonthlyPayment() <= incomeThreshold)
            {
                return "Approve!";
            }
            else
            {
                return $"Denied - monthly payment exceeds {INCOME_THRESHOLD * 100}% of total monthly income.";
            }
        }

        public override string ToString()
        {
            string output = $"\n***** RESULTS *****\nMonthly Income: ${MonthlyIncome}\nPurchase Price: ${PurchasePrice}\nMarket Value: ${MarketValue}\n" +
                $"Down Payment: ${DownPayment}\nInterest Rate %: {InterestRate * 100}\nNumber of Payments per Year: {PaymentsPerYear}\n" +
                $"Loan term (years): {Term}\nHOA Fees: ${HOA_Fee}\nLoan Base Amount: ${CalculateBaseLoanAmount()}\n" +
                $"Origination Fee: ${CalculateOriginationFee()}\nClosing Costs: ${TAXES_AND_CLOSING_CLOSTS}\n\n" +
                $"Total: ${CalculateTotalLoanAmount()}\n\nEquity Percent: {CalculateEquityPercentage()}%\nEquity Value: ${CalculateEquityValue()}\n\n" +
                $"Recommendation: {ApprovalRecommendation()}\n\nBase monthly: ${CalculateBasePayment()}\nInsurance: ${CalculateLoanInsurancePayment()}\n" +
                $"HOA Fees: ${CalculateHOAPayment()}\nTaxes and Escrow: ${CalculateEscrowPayment()}\n\nTotal monthly payment: ${CalculateTotalMonthlyPayment()}";
            return output;
        }
    }
}
