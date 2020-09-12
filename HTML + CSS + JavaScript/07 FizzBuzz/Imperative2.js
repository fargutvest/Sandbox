function isFizz(n) {
	if (n % 3 === 0) {
		return true;
	}
	return false;
}

function isBuzz(n) {
	if (n % 5 === 0) {
		return true;
	}
	return false;
}


function isFizzBuzz(n) {
	if (n % 3 === 0 && n % 5 === 0) {
		return true;
	}
	return false;
}

for ( let i = 1; i < 100; i++) {
	switch(true) {
		case isFizzBuzz(i):
			console.log('FizzBuzz');
			break;
		case isFizz(i):
			console.log('Fizz');
			break;
		case isBuzz(i):
			console.log('Buzz');
			break;
		default :
			console.log(i);
	}
}
