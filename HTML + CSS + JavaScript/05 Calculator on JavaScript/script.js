var firstOerand = 0;
var currentDigit = 0;
var lastButton;
var previousLastButton;
var waitNextOperand = false;
var memory;

function OnLoad(){
	document.forma1.calcDisp.value = 0;
}

function ButtonClick(button){
	if (button.alt == "comma"){
		if (document.forma1.calcDisp.value && document.forma1.calcDisp.value.indexOf('.') == -1){
			document.forma1.calcDisp.value = document.forma1.calcDisp.value + ".";
	    }	
    }


	if (button.alt == "Mplus"){
		if (document.forma1.calcDisp.value){
			memory = parseFloat(document.forma1.calcDisp.value);	
		}
		return;
	}

	if (button.alt == "Mminus"){
		if (memory){
			document.forma1.calcDisp.value = parseFloat(memory);
			memory = '';
		}
			return;
		}

	if (button.alt == "C"){
		document.forma1.calcDisp.value = 0;
		firstOerand = 0;
        currentDigit = 0;
        lastButton = '';
        previousLastButton = '';
        waitNextOperand = false;
		return;
	}

	if (lastButton && lastButton.alt =="result"){
		document.forma1.calcDisp.style["font-weight"] = '';
		lastButton = '';
		if (button.alt == "digit"){
			document.forma1.calcDisp.value = '';
		}
		else if (button.alt == "result" && previousLastButton.alt=="func"){
		lastButton = previousLastButton;
		firstOerand = parseFloat(document.forma1.calcDisp.value); 
		}
	}

	if (waitNextOperand){
	document.forma1.calcDisp.value = '';
	waitNextOperand = false;
	}	

	if (button.alt=="digit"){
		if (document.forma1.calcDisp.value == "0"){
			document.forma1.calcDisp.value = '';
		}
		document.forma1.calcDisp.value = document.forma1.calcDisp.value + button.value;
		currentDigit = parseFloat(document.forma1.calcDisp.value);
	}
	
	if (button.alt == "func"){
	firstOerand = parseFloat(document.forma1.calcDisp.value);
	lastButton = button;
	waitNextOperand = true;
	}

	if (button.alt == "result"){
	var result;
	
	switch (lastButton.name){
	case "plus":
	result = firstOerand + currentDigit;
	break
	case "minus":
	result = firstOerand - currentDigit;
	break
	case "multiply":
	result = firstOerand * currentDigit;
	break
	case "divide":
	result = firstOerand / currentDigit;
	break
	}
	previousLastButton = lastButton;
	lastButton = button;
	document.forma1.calcDisp.value = result;
	document.forma1.calcDisp.style["font-weight"] = "bold";
	}

}
