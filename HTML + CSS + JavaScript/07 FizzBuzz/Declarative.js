/*
* Решение FizzBuzz в функциональном стиле
*/

const gen = (n, w) => (num) => num % n === 0 ? w : '';
const fizz = gen(3, 'Fizz');
const buzz = gen(5, 'Buzz');

[...Array(99).keys()].map(i => i+1).forEach(i => console.log(fizz(i)+buzz(i) || i));