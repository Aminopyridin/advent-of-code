const rawInput = require('./inputs/02');

function calcFirstPartTest(data) {
    const input = data.trim().split(',').map(Number);

    let index = 0;
    while(input[index] !== 99) {
        stepVirtualMashine(index, input)
        index += 4;
    }

    return input;
}

function calcFirstPart() {
    const input = rawInput.trim().split(',').map(Number);
    input[1] = 12;
    input[2] = 2; 

    let index = 0;
    while(input[index] !== 99) {
        stepVirtualMashine(index, input)
        index += 4;
    }

    return input[0];
}

function calcSecondPart(resulted) {
    const input = rawInput.trim().split(',').map(Number);
    input[1] = 82;
    input[2] = 50; 

    let index = 0;
    while(input[index] !== 99) {
        stepVirtualMashine(index, input)
        index += 4;
    }

    console.log(input[0] === resulted, 100 * 82 + 50);
    return input[0];
}

function stepVirtualMashine(index, input) {
    const firstIndex = input[index + 1];
    const secondIndex = input[index + 2];
    const resultIndex = input[index + 3];

    if (input[index] === 1) {
        input[resultIndex] = input[firstIndex] + input[secondIndex];
    } else if (input[index] === 2) {
        input[resultIndex] = input[firstIndex] * input[secondIndex];
    }
}

console.log('test1', calcFirstPartTest('1,0,0,0,99'))
console.log('test2', calcFirstPartTest('2,3,0,3,99'))
console.log('test3', calcFirstPartTest('2,4,4,5,99,0'))
console.log('test4', calcFirstPartTest('1,1,1,4,99,5,6,0,99'))

const res = calcFirstPart();
console.log(res);

const output = 19690720;
const res2 = calcSecondPart(output);
console.log(res2);