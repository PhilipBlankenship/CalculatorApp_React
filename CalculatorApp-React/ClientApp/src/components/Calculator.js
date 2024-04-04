import React, { Component } from 'react';

export class Calculator extends Component {
    static displayName = Calculator.name;

    constructor(props) {
        super(props);
        this.state = { currEntry: "0", num1: null, num2: null, operator: "+" };
        this.updateEntry = this.updateEntry.bind(this);
        this.setOperator = this.setOperator.bind(this);
        this.executeMath = this.executeMath.bind(this);
    }

    updateEntry(inp) {
        if (this.state.currEntry == "0")
            this.setState({ currEntry: inp.toString() });
        else
        this.setState({
            currEntry: this.state.currEntry + inp
        });
    }

    addDot() {
        if (this.state.currEntry == "0") {
            this.setState({ currEntry: "0." });
        }
        else if (this.state.currEntry.includes(".")) {
            //do nothing 
        } else {
            this.setState({ currEntry: this.state.currEntry + "." });
        }
    }

    setOperator(inp) {
        if (this.state.num1 == null) {
            this.setState({ num1: parseFloat(this.state.currEntry), currEntry: "0", operator: inp });
        }
        else {
            this.setState({ num2: parseFloat(this.state.currEntry), currEntry: "0", operator: inp });
            this.executeMath();
        }
    }

    clear() {
        this.setState({
            currEntry: "0",
            num1: null,
            num2: null
        });
    }

    async store() {
        await fetch(`https://localhost:5001/api/calculator/StoreNumber?num=${this.state.currEntry}`, { method: "POST", headers: { "accept": "text/plain" } });
        this.setState({ currEntry: "0" });
    }

    async recall() {
        const response = await fetch('https://localhost:5001/api/calculator/RetrieveNumber', { method: "GET", headers: {"accept" : "text/plain"} });
        const data = await response.json();
        console.log(data);
        this.setState({ currEntry: data });
    }

    async executeMath() {
        let secondNumber = this.state.num2;
        if (secondNumber == null)
            secondNumber = this.state.currEntry;
        let url = "";

        switch (this.state.operator) {
            case '+':
                url = `https://localhost:5001/api/calculator/AddNumbers`;
                break;
            case '-':
                url = `https://localhost:5001/api/calculator/SubtractNumbers`;
                break;
            case '/':
                url = `https://localhost:5001/api/calculator/DivideNumbers`;
                break;
            case '*':
                url = `https://localhost:5001/api/calculator/MultiplyNumbers`;
                break;
        }

        url += `?num1=${this.state.num1}&num2=${secondNumber}`
        const response = await fetch(url, { method: "POST", headers: { "accept": "text/plain" } });
        const data = await response.json();
        this.setState({ num1: null, num2: null, currEntry: data });
        
    }

    render() {
        return (
            <div>
                <h1>Calculator</h1>

                <p aria-live="polite"><strong>{ (this.state.num1 != null && this.state.operator != null) ? this.state.num1 + " " + this.state.operator + " " + this.state.currEntry : this.state.currEntry}</strong></p>

                <button className="btn btn-primary" onClick={() => this.addDot()}>.</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('0')}>0</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('1')}>1</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('2')}>2</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('3')}>3</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('4')}>4</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('5')}>5</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('6')}>6</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('7')}>7</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('8')}>8</button>
                <button className="btn btn-primary" onClick={() => this.updateEntry('9')}>9</button>

                <button className="btn btn-primary" onClick={() => this.setOperator('+')}>+</button>
                <button className="btn btn-primary" onClick={() => this.setOperator('-')}>-</button>
                <button className="btn btn-primary" onClick={() => this.setOperator('*')}>*</button>
                <button className="btn btn-primary" onClick={() => this.setOperator('/')}>/</button>


                <button className="btn btn-primary" onClick={() => this.clear()}>CLEAR</button>
                <button className="btn btn-primary" onClick={() => this.store()}>STORE</button>
                <button className="btn btn-primary" onClick={() => this.recall()}>RECALL</button>
                <button className="btn btn-primary" onClick={() => this.executeMath()}>=</button>
            </div>
        );
    }
}
