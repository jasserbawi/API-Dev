let testBalloon 

function setup() {
    let canvas = createCanvas(640, 480)
    canvas.parent("gameContainer")
    testBalloon = new Balloon()
}

function draw() {
    //a nice sky blue background
    background(135, 206, 235)

    fill(testBalloon.col)
    circle(testBalloon.x, testBalloon.y, testBalloon.r)
}

class Balloon {
    constructor() {
        this.x = random(width)
        this.y = random(height)
        this.r = 25
        this.vx = 0
        this.vy = 0
        this.col = color(random(255), random(255), random(255))
        this.popped = false
    }
}
