var deckArray = [];
var player1Cards = [];
var player2Cards = [];
var suitsArray = ['♠︎', '♥︎', '♣︎', '♦︎'];
var cardValues = ['ACE', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'jack', 'queen', 'king'];
var cardPoints = [100, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];
var tie = false;
var cardsOnThisMoveForPlayer1 = [];
var cardsOnThisMoveForPlayer2 = [];
var movementIndex = 0;
var autoPlay;

/**
* Creates the principal deck.
*/
function createDeck() {
    deckArray = [];
    for (var i = 0; i < suitsArray.length; i++) {
        for (var j = 0; j < cardValues.length; j++) {
            var card = {
                suit: suitsArray[i],
                value: cardValues[j],
                points: cardPoints[j],
                isVisible:false
            };
            deckArray.push(card);
        }
    }
}

/**
* Shuffle the principal deck.
*/
function shuffleDeck() {
    for (var i = 0; i < deckArray.length; i++) {
        var swapIndex = Math.trunc(Math.random() * deckArray.length);
        var temp = deckArray[swapIndex];
        deckArray[swapIndex] = deckArray[i];
        deckArray[i] = temp;
    }
}

/**
* Shuffle the Player 1 deck.
*/
function shuffleP1Deck() {
    for (var i = 0; i < player1Cards.length; i++) {
        var swapIndex = Math.trunc(Math.random() * player1Cards.length);
        var temp = player1Cards[swapIndex];
        player1Cards[swapIndex] = player1Cards[i];
        player1Cards[i] = temp;
    }
}

/**
* Shuffle the Player 2 deck.
*/
function shuffleP2Deck() {
    for (var i = 0; i < player2Cards.length; i++) {
        var swapIndex = Math.trunc(Math.random() * player2Cards.length);
        var temp = player2Cards[swapIndex];
        player2Cards[swapIndex] = player2Cards[i];
        player2Cards[i] = temp;
    }
}

/**
* Prints a card name.
*/
function getCardString(card) {
    return card.value + ' of ' + card.suit;
}

/**
* Gets the next card in the principal deck.
*/
function getNextCard() {
    return deckArray.shift();
}

/**
* Prints the cards for both players in the DOM.
*/
function PrintCardsOnPlayerDashboards() {
    $("#player1-cards-number").html(player1Cards.length);
    $("#player2-cards-number").html(player2Cards.length);
    $("#player1-cards").html("");
    $("#player2-cards").html("");
    player1Cards.forEach(function (card) {
        $("#player1-cards").append("<label>" + card.value + " " + card.suit + " SHOW: " + card.isVisible + "</label></br>")
    });

    player2Cards.forEach(function (card) {
        $("#player2-cards").append("<label>" + card.value + " " + card.suit + " SHOW: " + card.isVisible + "</label></br>")
    });

}

/**
* Starts a new game
*/
function NewGame() {
    console.log('Welcome to WAR!');
    createDeck();
    shuffleDeck();
    player1Cards = [];
    player2Cards = [];
    for (var i = 0; i < 26; i++) {
        player1Cards.push(getNextCard());
        player2Cards.push(getNextCard());
    }
    PrintCardsOnPlayerDashboards();
}


/**
* Turns up the card for the current movement
* and prints the current cards for the current movement.
*/
function ShowUpCards() {
    // turn up the card
    cardsOnThisMoveForPlayer1[movementIndex].isVisible = true;
    cardsOnThisMoveForPlayer2[movementIndex].isVisible = true;


    $("#player1-current-card").html("");
    $("#player2-current-card").html("");

    cardsOnThisMoveForPlayer1.forEach(function (card) {
        let cardForP1 = card.suit + " " + card.value + " VISIBLE: " + card.isVisible;
        $("#player1-current-card").append("<label>"+cardForP1+ "</label><br>");

    });

    cardsOnThisMoveForPlayer2.forEach(function (card) {
        let cardForP2 = card.suit + " " + card.value + " VISIBLE: " + card.isVisible;
        $("#player2-current-card").append("<label>" + cardForP2 + "</label><br>");

    });

    console.log('Player 1 card ' + getCardString(cardsOnThisMoveForPlayer1[movementIndex]));
    console.log('Player 2 card ' + getCardString(cardsOnThisMoveForPlayer2[movementIndex]));
}

/**
* Put facedown the cards for the current move.
*/
function TurnDownCards() {
    cardsOnThisMoveForPlayer1.forEach(function (card) {
        card.isVisible = false;
    });

    cardsOnThisMoveForPlayer2.forEach(function (card) {
        card.isVisible = false;
    });
}

/**
* Execute the next movement.
*/
function NextMove() {
    if (player1Cards.length == 0 || player2Cards.length == 0) {
        alert("Please start a new game");
        StopAutoPlay();
        return;
    }

    // restart cards on this move it was not a tie
    if (!tie) {
        cardsOnThisMoveForPlayer1 = [];
        cardsOnThisMoveForPlayer2 = [];
        movementIndex = 0;
    }

    // distribute new cards
    cardsOnThisMoveForPlayer1.push(player1Cards.shift());
    cardsOnThisMoveForPlayer2.push(player2Cards.shift());

    ShowUpCards();

    // Clean dashboard colors
    $("#player1-dashboard").css("background-color", "transparent")
    $("#player2-dashboard").css("background-color", "transparent")
    $("#central-dashboard").css("background-color", "transparent")


    //Check winner
    if (cardsOnThisMoveForPlayer1[movementIndex].points > cardsOnThisMoveForPlayer2[movementIndex].points) {
        // player 1 wins
        console.info("Player 1 wins");
        TurnDownCards();
        player1Cards = player1Cards.concat(cardsOnThisMoveForPlayer1).concat(cardsOnThisMoveForPlayer2);
        $("#player1-dashboard").css("background-color", "green")
        tie = false;
    } else {
        if (cardsOnThisMoveForPlayer1[movementIndex].points == cardsOnThisMoveForPlayer2[movementIndex].points) {
            // tie
            console.warn("TIE");

            // takes the card that will be upside down
            cardsOnThisMoveForPlayer1.push(player1Cards.shift());  
            cardsOnThisMoveForPlayer2.push(player2Cards.shift());
            $("#central-dashboard").css("background-color", "yellow")
            tie = true;
            movementIndex = movementIndex + 2;
        } else {
            // player 2 wins
            console.info("Player 2 wins");
            TurnDownCards();
            player2Cards = player2Cards.concat(cardsOnThisMoveForPlayer1).concat(cardsOnThisMoveForPlayer2);
            $("#player2-dashboard").css("background-color", "green")
            tie = false;
        }
    }

    PrintCardsOnPlayerDashboards();

    if (player1Cards.length == 0) {
        player2Cards = player2Cards.concat(cardsOnThisMoveForPlayer1).concat(cardsOnThisMoveForPlayer2);
        StopAutoPlay();
        alert("Player 2 has won the game");
    }
    if (player2Cards.length == 0) {
        player1Cards = player1Cards.concat(cardsOnThisMoveForPlayer1).concat(cardsOnThisMoveForPlayer2);
        StopAutoPlay();
        alert("Player 1 has won the game")
    }
}

/**
* Used to start the play in automatic mode.
*/
function StartAutoPlay() {
    autoPlay = setInterval(() => NextMove(), 250);
}

/**
* Stop the automatic mode.
*/
function StopAutoPlay() {
    clearInterval(autoPlay);
}