using TechTalk.SpecFlow;
using CalculatorBDDDemoCalculator;

namespace CalculatorBDDDemo
{
    [Binding]
    public class CalculatorStepDefinitions
    {
        private Calculator _calc;
        private int _result;

        [Given(@"I have a calculator")]
        public void GivenIHaveACalculator()
        {
            _calc = new();
        }

        [Given(@"I enter (.*) and (.*) into the calculator")]
        public void GivenIEnterAndIntoTheCalculator(int a, int b)
        {
            _calc.A = a;
            _calc.B = b;
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _result = _calc.Add();
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expected)
        {
            Assert.That(_result, Is.EqualTo(expected));
        }

        [When(@"I press subtract")]
        public void WhenIPressSubtract()
        {
            _result = _calc.Subtract();
        }
        
        [When(@"I press multiply")]
        public void WhenIPressMultiply()
        {
            _result = _calc.Multiply();
        }

        [When(@"I press divide")]
        public void WhenIPressDivide()
        {
            _result = _calc.Divide();
        }

        [Then(@"a DivideByZero Exception should a DivideByZeroException when I press divide")]
        public void ThenADivideByZeroExceptionShouldADivideByZeroExceptionWhenIPressDivide()
        {
            _result = _calc.Divide();
        }

        [Then(@"the exception should have the message ""([^""]*)""")]
        public void ThenTheExceptionShouldHaveTheMessage()
        {
            Assert.That(() => _result, Throws.TypeOf<DivideByZeroException>());
        }

    }
}
