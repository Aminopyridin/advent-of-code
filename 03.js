const rawInput = require('./inputs/03');

function run() {
    const [first, second] = rawInput.trim().split('\n');
    const firstPath = first.trim().split(',');
    const secondPath = second.trim().split(',');

    console.log(firstPart(firstPath, secondPath));
    console.log(secondPart(firstPath, secondPath));
}

function firstPart(firstPath, secondPath) {
    const pathPoints = createPathPointsSet(firstPath);
    const crosses = findCrosses(pathPoints, secondPath);
    return findClosest(crosses);
}

function secondPart(firstPath, secondPath) {
    const pathPoints = createPathPointsDict(firstPath);
    return findShorterPathToCross(pathPoints, secondPath);
}

function createPathPointsSet(path) {
    const pathPoints = new Set();

    goThroughPathSegments(path, point => pathPoints.add(point))
    
    return pathPoints;
}

function createPathPointsDict(path) {
    const pathPoints = {};

    goThroughPathSegments(path, (point, step) => {

        if (!(point in pathPoints)) {
            pathPoints[point] = step;
        }
    })
    
    return pathPoints;
}

function findCrosses(set, path) {
    const crossPoints = new Set();

    goThroughPathSegments(path, point => {
        if (set.has(point)) {
            crossPoints.add(point)
        }
    });
    
    return crossPoints;
}

function findClosest(crosses) {
    let minDistance = Number.MAX_SAFE_INTEGER;
    for (const point of crosses) {
        const [x, y] = point.split('_').map(Math.abs);
        const sum = x + y;
        minDistance = Math.min(minDistance, sum)
    }
    return minDistance;
}

function findShorterPathToCross(dict, path) {
    let minPath = Number.MAX_SAFE_INTEGER;

    goThroughPathSegments(path, (point, step) => {
        if (point in dict) {
            const sum = step + dict[point];
            minPath = Math.min(minPath, sum);
        }
    });

    return minPath;
}

function goThroughPathSegments(path, callback) {
    let x = 0;
    let y = 0;
    let step = 0;

    for (const segment of path) {
        const move = createMove(segment);
        const distance = parseInt(segment.substring(1), 10);

        for (let i = 1; i <= distance; i++) {
            ++step;
            x += move.x;
            y += move.y;
            callback(`${x}_${y}`, step);
        }
    }
}

function createMove(segment) {
    const res = {x: 0, y: 0};

    const direction = segment[0]

    if (direction === "R") res.x = 1;
    if (direction === "L") res.x = -1;
    if (direction === "D") res.y = -1;
    if (direction === "U") res.y = 1;


    return res;
}

run();