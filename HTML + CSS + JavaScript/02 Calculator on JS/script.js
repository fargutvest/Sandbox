var firstOerand = 0;
var lastButton;
var waitNextOperand = false;

function ButtonClick(button){

	if (waitNextOperand){
	document.forma1.calcDisp.value = "";
	waitNextOperand = false;
	}	

	if (button.alt=="digit")
	document.forma1.calcDisp.value = document.forma1.calcDisp.value + button.value;

	if (button.alt == "func"){
	firstOerand = parseInt(document.forma1.calcDisp.value);
	lastButton = button;
	waitNextOperand = true;
	}

	if (button.alt == "result"){
	var result;
	var current = parseInt(document.forma1.calcDisp.value);
	switch (lastButton.name){
	case "plus":
	result = firstOerand + current;
	break
	case "minus":
	result = firstOerand - current;
	break
	case "myltiply":
	result = firstOerand * current;
	break
	case "divide":
	result = firstOerand / current;
	break
	}
	document.forma1.calcDisp.value = result;
	}

}
