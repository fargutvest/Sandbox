var mem = 0;
var lastButton;
var waitNextOperand = false;

function ButtonClick(button){

	if (button.alt!="func")
		if (!waitNextOperand)
	document.forma1.calcDisp.value = document.forma1.calcDisp.value + button.value;
		else
	document.forma1.calcDisp.value = button.value;
	else
		if (button.name != "result")
mem = parseInt(document.forma1.calcDisp.value);

	if (button.name != "result" && button.alt =="func"){
lastButton = button;
waitNextOperand = true;
}

if (button.name == "result")
{
	var result;
	var current = parseInt(document.forma1.calcDisp.value);
switch (lastButton.name){
	case "plus":
	result = mem + current;
	break
	case "minus":
	result = mem - current;
	break
	case "multiply":
	result = mem * current;
	break
	case "divide":
	result = mem / current;
	break
}
document.forma1.calcDisp.value = result;
}

}
