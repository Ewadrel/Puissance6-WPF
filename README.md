# Connect 6 - A WPF Game

**Connect 6** is a modern twist on the classic Connect 4 game, where the goal is to connect **6 tokens** in a row instead of 4. The game is built using **WPF** and developed in **C#**.

## Features

- 🎮 **Local Multiplayer Mode**: Two players can compete by taking turns.
- 🕑 **Built-in Timer**: Each player has a limited time to make their move.
- 🎨 **Modern Interface**: Sleek and intuitive design powered by WPF.
- 🟢 **Optional Special Bonuses**: Add strategic bonuses to make the game more exciting.
- 🏆 **Victory/Defeat Screen**: A dedicated window announces the winner or declares a tie.
- 🤖 **AI Mode (Artificial Intelligence)**: Challenge an AI opponent with simple strategies.
- 🏆 **Win Condition**: The first player to connect 6 tokens horizontally, vertically, or diagonally wins the game.

## Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/Ewadrel/Puissance6-WPF.git
   ```

2. **Open the project in Visual Studio**:
   - Ensure you have **Visual Studio 2022** (or a compatible version) with .NET tools installed.

3. **Run the application**:
   - Launch the project in either Debug or Release mode.

## Game Rules

1. Players take turns dropping a token of their color (e.g., purple or green).
2. The token automatically falls to the lowest available position in the chosen column.
3. The first player to align **6 consecutive tokens** horizontally, vertically, or diagonally wins the game.
4. If the grid is completely filled and no one wins, the game ends in a **draw**.

## Additional Features

### Victory/Defeat Screen
At the end of each game, a window pops up to display:
- **The winner**, highlighted with their color.
- If the game ends in a draw, a specific message is shown.

### AI Mode
A single-player mode allows you to compete against the computer. The current AI uses basic strategies, making it unpredictable but beatable. This mode is perfect for practice or solo play.

## Technologies Used

- **Language**: C#
- **Framework**: WPF (Windows Presentation Foundation)
- **IDE**: Visual Studio 2022

## Future Development

- ✅ Add a more advanced AI with different difficulty levels.
- ✅ Implement save and load functionality for ongoing games.
- ✅ Include statistics tracking (wins/losses).
- ✅ Add sound effects for interactions.
