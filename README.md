# WinFormsMatchingGame

This is an in-development app, demonstrating some accessibility-related topics.

Please note that the below was Guy Barker's understanding of things at the time of writing all this, and some of it might not be right. If someone knows of any errors in what I've said, please let me know. I also want to learn.

**Goal**

The goal of this app is to demonstrate some considerations and implementation relating to an simple, accessible Windows game app.

**Playing the game**

The game is based on a traditional card matching game, where face-down cards are turned over in the hope of finding pairs of cards.

When the game is run, a 4x4 grid of squares appears, each square representing a card. When a card is clicked, an image is shown on the square. When another square is clicked, an another is also shown on that square. If the two images are the same, then the squares are considered to be matched, and will not change again for the rest of the game. If the images do not match, then the cards are considered to be unmatched, and the "Turn cards back over" button must be clicked, which results in the two unmatched cards no longer showing images. These steps are to be repeated until all matching images have been found, and a "Congratulations" window appears.

**Keyboard**

Tab key presses move keyboard focus between the grid and the buttons shown in the app. To move keyboard focus between the cards inside the grid, use the arrow keys. To turn two unmatched cards back over, either press Alt+A or the Enter key.

Is it recognized that the game needs work to explain to a player exactly how the game can be played. For example, there is no information in the game related to the results of the pressing the Enter key while keyboard focus is on a card in the grid.

**Speech**

To click one of the cards shown in the app using speech input, say "Click Card" followed by the number of the card. The cards are numbered from 1 to 16, reading let to right then top to bottom. For example, to click the first card on the bottom row, say "Click card 13".

**Technical considerations while building the app**

By default, when building games for that everyone can use, it's best to use standard controls with come with the UI framework being used. Typically standard controls have built-in support for some level of accessibility, and so provide a great head-start when building an accessible app. So the first question is, what would be the most appropriate WinForms control to use for the grid of cards?

One option that seemed attractive was to use a DataGridView control. That supports keyboard accessibility, customization of both the programmatic and visual representation of its cells, and programmatically exposed the row and column data associated with the grid and cells withing the grid. As such, the DataGridView seemed to have great potential as a starting point for the grid shown in the game. The DataGridView typically has data bound to it and populates rows in the grid based on the data items, but for this app, each cell is independent of all other cells, and so data binding isn't used. Rather, a separate list of card data is managed independently of grid, and the list's data accessed however necessary when working with the grid.

The next question is what type of DataGridView cells are the best match for the cards in the game. Given that images are to be shown in the cells, it was tempting to use DataGridViewImageCell. That would make it really easy to have images shown, and to specify how the images are to be presented. However, in tests, it didn't seem that the DataGridViewImageCell responded to attempts to click the cell with the keyboard or through speech input. So instead, the DataGridView in the game uses the DataGridViewButtonCell. That type of cell responds to a click using mouse, touch, keyboard and speech, and so provides a great head start on supporting all players. The only apparent disadvantage of using the DataGridViewButtonCell here is that it doesn't natively support displaying an image. As such, the DataGridView in the game has its CellPainting overridden, and manual action is taken to present the image on a card.

I was prompted to build this game after finding an inaccessible card matching game. One of the ways in which that app was inaccessible was its use of hard-coded timeouts. Cards would automatically be turned over after a short period, which means that in practice some players would not have access to the information temporarily being shown. At some point, I might add a timeout, but if I do, the player must be completely in control of the timeout, including having the option to have not timeout. Rather than adding a hard-coded timeout now, with a claim that I'll make it configurable later, I chose to have not timeouts at all in the app. When anything happens in the game, it's because the player initiated it.

The game does in some situations raise programmatic events, requesting that screen reader make custom game-specific announcements. I would expect it would be preferable for these announcements to be made configurable, and so hopefully feedback from players can help the game's announcements become more useful. Currently, game-specific announcements are made in the following cases:

1. A click is made on any card when two unmatched cards are already showing.
2. A card it turned over and matches another card that's already turned over.
3. Two unmatched cards are turned back.

By default, I would say that the names of cards in the game should never change. For example, a card might have a name of "Card 1" or "Card 8". Any current state data associated with the card would be programmatically exposed through other properties, such as its Value. However, given that a card's Value might not be announced depending on a screen reader's settings, the very important data of what image is shown on the card, that data is incorporated into the name. Feedback would be appreciated as to how all the data associated with a card would be most helpfully exposed.

The Description property of a class derived from DataGridViewButtonCellAccessibleObject does not get exposed through the Windows UI Automation (UIA) API as as UIA clients would expect, so that property is not overridden. Rather, the Help property is used to describe the image shown on a card, and that gets exposed through the UIA HelpText property.


![The Accessibility Insights for Windows tool reporting the UI Automation hierarchy of the grid cells shown in the game.](WinFormsMatchingGame/AppScreenshots/WinFormsMatchingGameUIATree.png)


**Technical resources**

The following resources show code snippets relating to customizing the accessibility of DataGridView cells.
[WinForms: Setting the accessible name of a DataGridView cell](https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_Name)
[WinForms: Setting helpful supplemental information on a DataGridView cell](https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_HelpText)
[WinForms: Setting a value on a DataGridView cell](https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_ValueValue)

For other technical accessibility resources relating to WinForms and other Windows UI Frameworks, please visit 
[Common approaches for enhancing the programmatic accessibility of your Win32, WinForms and WPF apps](https://www.linkedin.com/pulse/common-approaches-enhancing-programmatic-your-win32-winforms-barker)

**To run the app without building it**

The app's exe has been made available for people who aren't set up to build the app themselves. To run this app, following the steps below. The app will only run on a 64-bit version of Windows.

1. Downloading the exe from the folder at [WinFormsMatchingGame](https://1drv.ms/u/s!AlVXdkIXfQVpidFhyGkmurWNrLifNA?e=gFMquj) to your computer. Note that typically exe files would never be downloaded from the cloud, so you would have to explicitly say at the download site that you wish the file to be downloaded.
2. Run the exe from your computer. Given that the exe is not digitally signed, you would have to explicitly say that you wish the app to run on your computer.
3. If .NET 5 is not yet installed on your computer, you will be prompted to install it. The required version is .NET for x64 Windows Desktop.
