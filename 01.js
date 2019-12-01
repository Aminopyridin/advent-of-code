const rawInput = require('./inputs/01.js');

function calcFuel() {
    const input = rawInput.trim().split('\n');
    
    return input.reduce((acc, item) => {
        const fuelPerMass = Math.trunc(item / 3) - 2;
        return acc + fuelPerMass;
    }, 0)
}

function calcFuel2() {
    const input = rawInput.trim().split('\n');
    
    return input.reduce((acc, item) => {
        const fuelPerMass = calcFuelPerMass(item);
        return acc + fuelPerMass;
    }, 0)
}

function calcFuelPerMass(mass) {
    let addFuel = Math.trunc(mass / 3) - 2;
    if (addFuel < 0) {
        return 0;
    }

    return addFuel + calcFuelPerMass(addFuel);
}

const fuelAmount = calcFuel();
console.log(fuelAmount);

