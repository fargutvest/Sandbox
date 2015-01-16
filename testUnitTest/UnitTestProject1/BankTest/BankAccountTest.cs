using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace UnitTestProject1
{
    // Атрибут [TestClass] является обязательным для платформы модульных тестов Microsoft для управляемого 
    // кода в любом классе, содержащем методы модульных тестов, которые необходимо выполнить в обозревателе тестов.
    // Каждый метод теста, который требуется выполнять с помощью обозревателя тестов, должен иметь атрибут [TestMethod].

    [TestClass]
    public class BankAccountTest
    {
        // Этот метод достаточно прост. 
        // Мы создаем новый объект BankAccount с начальным балансом, а затем снимаем допустимое значение. 
        // Используем платформу модульных тестов Microsoft для метода AreEqual управляемого кода, 
        // чтобы проверить соответствие конечного баланса ожидаемому. 
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert is handled by ExpectedException
        }

        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = 12.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert is handled by ExpectedException
        }

    }
}
