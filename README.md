# WinFormsMatchingGame

**Goals**

The goals of this app are to (1) demonstrate some considerations and implementation relating to a simple, accessible Windows game app, and (2) make an enjoyable game available for everyone.

&nbsp;

**Playing the game**

The game is based on a traditional card matching game, where face-down cards are turned over in the hope of finding pairs of cards.

When the game is run, a 4x4 grid of blank squares appears, each square representing a card. When a card is clicked, an image is shown on the card. When another card is clicked, an image is also shown on that second card. If the two images are the same, then the cards are considered to be matched, and will not change again for the rest of the game. If the images do not match, then the cards are considered to be unmatched, and the "Turn cards back over" button must be clicked, which results in the two unmatched cards no longer showing images. These steps are to be repeated until all matching images have been found, and a "Congratulations" window appears.

&nbsp;

The following image shows the game showing two pairs of matched cards and one pair of unmatched cards.

&nbsp;

![The WinFormsMatchingGame showing two pairs of matched cards and one pair of unmatched cards.](WinFormsMatchingGame/AppScreenshots/WinFormsMatchingGameMismatch.png)

&nbsp;

**Keyboard**

Tab key presses move keyboard focus between the grid and the buttons shown in the app. To move keyboard focus between the cards inside the grid, use the arrow keys. To turn two unmatched cards back over, either press Alt+B or the Enter key.

It is recognized that the app needs work to explain to a player exactly how the game can be played with the keyboard. For example, there is no information in the game related to pressing the Enter key while keyboard focus is on a card in the grid.

&nbsp;

**Speech**

To click one of the cards shown in the app using speech input, say "Click Card" followed by the number of the card. The cards are numbered from 1 to 16, reading left to right then top to bottom. For example, to click the first card on the bottom row, say "Click card 13".

The following image shows two pairs of matched cards and one pair of unmatched cards. Above the game is the Windows Speech Recognition feature showing "Clicking Card 10, Face down"

&nbsp;

![The WinFormsMatchingGame showing two pairs of matched cards and one pair of unmatched cards. Above the game is the Windows Speech Recognition feature showing "Clicking Card 10, Face down"](WinFormsMatchingGame/AppScreenshots/WinFormsMatchingGameSpeech.png)

&nbsp;

**Technical considerations while building the app**

By default, when building games for everyone to play, it's best to leverage standard controls which come with the UI framework being used to build the app. Typically standard controls have built-in support for some level of accessibility, and so they provide a great head start when building an accessible app. So the first question is, what would be the most appropriate WinForms control to use for the grid of cards?

Well, given that the game is basically a grid of cards, one option that seemed attractive was to use a DataGridView control. That control presents a grid of cells, and supports keyboard accessibility, fundamental programmatic accessibility of its cells (with some level of customization), and also programmatically exposes the row and column data associated with the grid and each cell. As such, the DataGridView seemed to have great potential as a starting point for the grid shown in the game. 

Typically a DataGridView control has its data bound, with each data item representing a single row in the grid. So the game app here could have a data item containing the data for all cells on a single row. For this early version of the game app however, given that each cell is independent of all other cells, data binding isn't used. Rather, internally a separate list of card data is managed independently of the grid, and that list's data is accessed however necessary when working with the grid. In later versions of the game, I expect data binding will be used.

Perhaps it might turn out that at some point another control seems a more attractive option for presenting the cards in the game. For example, using a ListView with a View property of LargeIcon can visually provide a grid which is keyboard accessible, programmatically accessible, and it's simple to have each item in the list bound to some data. One reason why the ListView control is not currently being used in the app today is because it doesn't seem to support the UIA Grid pattern when showing a grid of icons. As such, a cell in the grid doesn't programmatically expose its row and column data like a cell in a DataGridView control does.

The next question is what type of DataGridView cells are the best match for the cards in the game? Given that images are to be shown in the cells, it was tempting to use DataGridViewImageCell. That would make it really easy to have images shown, and to specify how the images are to be presented. However, in tests, it didn't seem that the DataGridViewImageCell responded to attempts to click the cell with the keyboard or through speech input. So instead, the DataGridView in the game uses the DataGridViewButtonCell. That type of cell responds to a click using mouse, touch, keyboard and speech, and so provides a great head start on supporting all players. The only apparent disadvantage of using the DataGridViewButtonCell here is that it doesn't seem to natively support displaying an image. As such, the DataGridView in the game has its CellPainting overridden, and manual action is taken to present the image on a card.

I was prompted to build this game after finding an inaccessible card matching game. One of the ways in which that app was inaccessible was its use of hard-coded timeouts. Face-up cards would automatically be turned over after a short period, which means that in practice some players would not have access to the information temporarily being shown. At some point, I might add use of a timeout, but if I do, players must be completely in control of the timeout, including having the option to have no timeout at all. Rather than me adding a hard-coded timeout now, with a claim that I'll make it configurable later, I chose to have no timeouts in this early version of the app. This means that when anything happens in the game, it's because the player chose to make it happen.

The game does in some situations raise programmatic events, requesting that screen readers make custom game-specific announcements. I would expect it would be preferable for these announcements to be made configurable, and so hopefully feedback from players can help the game's announcements become more useful. Currently, game-specific announcements are made in the following cases:

1. A click is made on any card when two unmatched cards are already showing.
2. A card is turned over and matches another card that's already face-up.
3. Two face-up unmatched cards are turned back over.

By default, I would say that the names of cards in the game should never change. For example, a card might have a name of "Card 1" or "Card 8", and its name would not change based on whether it’s face-up or face-down. Any current state data associated with the card would be programmatically exposed through other properties, such as its value. However, given that a card's value might not be announced due to a screen reader's current settings, and the image shown on the card is always essential when playing the game, that data is currently incorporated into the card's name. Feedback would be appreciated as to how all the data associated with a card would be most helpfully exposed to screen readers.

The Description property of a class derived from DataGridViewButtonCellAccessibleObject does not get exposed through the Windows UI Automation (UIA) API as UIA clients would expect it to, so that property is not overridden in the app. Rather, the Help property is used to describe the image shown on a card, and that gets exposed through the UIA HelpText property.

The following image shows the Accessibility Insights for Windows tool reporting the UI Automation hierarchy of the grid cells shown in the game.

&nbsp;

![The Accessibility Insights for Windows tool reporting the UI Automation hierarchy of the grid cells shown in the game.](WinFormsMatchingGame/AppScreenshots/WinFormsMatchingGameUIATree.png)

&nbsp;

**Technical resources**

The following resources show code snippets relating to customizing the accessibility of DataGridView cells.
- [WinForms: Setting the accessible name of a DataGridView cell](https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_Name) 
- [WinForms: Setting helpful supplemental information on a DataGridView cell](https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_HelpText) 
- [WinForms: Setting a value on a DataGridView cell](https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_ValueValue) 

For other technical accessibility resources relating to WinForms and other Windows UI Frameworks, please visit 
[Common approaches for enhancing the programmatic accessibility of your Win32, WinForms and WPF apps](https://www.linkedin.com/pulse/common-approaches-enhancing-programmatic-your-win32-winforms-barker)

&nbsp;

**To run the app without building it**

The app's available at the Microsoft Store for people who aren't set up to build the app themselves. To run this app, following the steps below. This exe will only run on a 64-bit version of Windows.

1. Download the app from the Microsoft store at [Accessible Matching Game](https://www.microsoft.com/store/apps/9P6KHLZJF7C7).
2. If .NET 5 is not yet installed on your computer, you will be prompted to install it when the app is run. The required version is .NET 5 for x64 Windows Desktop.

&nbsp;

**Future improvements**

Please provide feedback on how the app could be made more accessible to yourself or people you know. I'll try my best to update it accordingly. Thanks for your help!

