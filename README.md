ButterMouse Games Second Development
Design Document
----------------------------------

<img width="450" height="250" alt="unnamed (1)" src="https://github.com/user-attachments/assets/1bda19cc-85d9-41ef-9986-3eff97d71763" />
<img width="450" height="250" alt="unnamed" src="https://github.com/user-attachments/assets/abfe8e23-f54e-46ca-b192-c4a489882011" />

Development Team

Phoebe Pudge - Conceptual design and Programming - Lead Programmer
Conor Lewis - Programming
Berren Robin Stonechild - Art and design
Carlton Grerkgied Cunliffe - Art and design - Lead Designer


1. Index
Overview
Context to prior design
Brief of concept
Concept
Initial Baseline of Concept
Potential in Developments
Mechanics
Exploration
Combat
Crafting
Management
Socializing
Visuals
3D Assets
Animation
2D Assets
Shaders
Narrative Elements
Setting
Characters
Code
Relevant Technologies and Process
Miscellaneous Elements
Miscellaneous Sound Design
Music
Development
Stages of development
Timeframe
Expansions of Scale
Working Expectations
Business
Marketing
Finances
Licensing of Software
Publishing


2. Overview

Context of Prior Design
This section intends to provide an explanation as to why the team is now pursuing a second design. Due to a number of reasons the team over the course of a few months has lost four of its original eight members. The team's prior venture was planned and designed with a larger group than what is now present and there was a need to manage scale so that there was an achievable goal in regards to the production. In addition to this a number of design challenges were poorly handled leading the design to become lost within the view of the team leading to motivational issues faced by all. The remaining team now intends to use the failures of the previous concept in order to produce a clearer design and more focused team through a number of solutions described later on within the document. In addition, we plan to reuse code from the previous project where appropriate, to minimize the time lost on the previous project.

 Brief of Concept
The second design we intend to produce is a dungeon crawler with crafting, management and social mechanics. The gameplay will focus on a cycle of obtaining ore from a dungeon, processing and crafting this ore into weapons and armor, then using these products to explore further into the dungeon and gain more valuable materials.
In this game you will make and sell equipment , and purchase materials.. In order to create this equipment you will have to venture through a procedural dungeon. Outside of exploring the dungeon and crafting, the player will be able to interact with a small roster of NPCs.


3. Concept

Initial Baseline of Concept
This section intends to further elaborate on the concept described in section 2b. The game's dungeon crawler mechanic intends to have the player venture through a procedural dungeon fighting enemies and mining ores and materials. 

With said materials they may return to their home to craft equipment through a number of systems of simplified metallurgy. From this they may either use this equipment in the dungeon or sell them for currency. This serves as the central gameplay loop. 

Aside from the central gameplay loop will exist a set of social interactions with NPC’s in order to ground context to the player's actions alongside providing a break from the monotony of the gameplay loop. These social events will work around an ingame schedule and a meter of affection from each NPC. The focus of this game's concept and design is to provide an experience for a niche audience of players who enjoy games similar to Stardew valley and Moonlighter by following trends of said titles and titles similar to them. The experience intends to be unique by applying a new flavor and combination of these mechanics and providing small innovations.

Potential in Developments
A major concern of our team is the management of scale in contrast to the innovation of mechanics and ideas. In an attempt to address this concern within the planning elements of our design we have decided to take an approach in which we set a specific baseline for each mechanic and element for our game which is the forefront of our aim. These baselines are intended to be as small scale as possible while achieving the desired experience at the most basic level possible. In the further description of mechanics in section 4 there will be an explicit statement of our baseline for development alongside separate details going over possible further development.


4. Mechanics

Exploration
The first main mechanic of our game is exploration. Within this mechanic we intend to provide varied environments with an amount of detail for our player to interact with. 

In order to provide this our game will provide a procedural dungeon for the player to delve into. There will be multiple levels to this dungeon, and players can exit the dungeon on any floor, and get to the next floor by finding the entrance. In this dungeon the game will provide randomly generated maps populated with mineable materials and enemies for the player to find and interact accordingly. Materials will need various qualities of tools to mine, with higher tier materials found on deeper levels, with stronger monsters too.

This exploration is an integral part of the overall gameplay loop as it is an interaction necessary for the player to engage with to pursue other aspects of that loop and progress further into the game with. At this point in the design our main intent for this dungeon is to get the procedural map and object placement functional alongside designing a short amount of progression of layers.

Ideas produced by the group for further development:
Variation to the size of each level. An example could be that a standard level is 50 x 50 wide, however every 10 floors, you encounter a floor that is 100 x 100. Or that the lower down you get, the bigger the floor is. An example calculation could be 50 + (floor level * 2), so level 10 would have a size of 70 x 70, and level 50 would have a size of 250 x 250. Larger room sizes allow for more opportunities for materials to spawn, however also increases the amount of monsters that are able to spawn.
Different visual aesthetics depending on the level, this can be simple things such as changing the wall and floor materials, and allowing different types of monsters to appear. An example could be, for floors 20 - 30, the walls of the cave are made from a dark red brick, fire monsters appear in the surrounding areas, and materials such as volcanic rocks and quarts can be found commonly. 
A deformation system, in which the player can mine away the walls of the dungeon to get through the level quicker. Perhaps this can be done by crafting a special type of tool, such as a hammer 
Given how the level is generated procedurally, each level is generated from a random seed so it will be unique. Our current vision is for the level to be regenerated when you exit the dungeon, so each dungeon run will have a different generation.
Our dungeon level system will be using ladders / stairways, so that upon generation, a way down (such as a stairway or ladder), will appear. Our plan is to make a safe level every few, in which the player can return to the surface from. 
Consumable teleportation items for use to move between the mine and the village. This idea is described as a system in which the player may purchase consumable items (in my mind, potions) for the purpose of traversal in a similar way to the scrolls in Torchlight II. The player will be able to purchase three separate sets of consumables: A one way return, a one use portal and a one way trip. The one way return is a usable item to return to the village from the mine. The second the one use portal which allows the player to return to the village and then to the place in the mine they used the potion from. The third the one way trip which allows the player to fast travel to a specific point unlocked in the mine.

Combat
The second main mechanic of the game is combat. Within the aforementioned procedural dungeon the player will encounter a number of enemies with whom they will combat. The combat at this moment is intended to function as a top-down hack and slash type of gameplay. At this moment the intended design has not been consolidated in favor of developing other elements, and is intended to be expanded upon later on, when we have a further idea of our available timeline. 

The baseline combat will allow you to attack your enemy with basic slashes of the sword, against various enemies. You can equip different weapons for different attacking statistics, such as speed, attack damage, critical hit chance and more. 

Ideas produced by the group for further development:
Status effects dependent on the weapon used, such as a sword created from fiery material, may create a burn effect where fire particles surround the enemy, and a small amount of damage is inflicted over a period of time.
Give the player the ability to defend themselves with a shield; different shields can be crafted to give different effects or to improve the damage blocking potential. 
Larger-scale enemies that can have a small chance to spawn and are limited to one per floor, to act as a chance boss, or set bosses to appear on specific floors. This is more a potential idea, and will be completed towards the end of the project should the time scale allow it. 


Crafting
The third mechanic this game will focus on is that of a crafting system. The crafting system in place is a heavily simplified version of real world metallurgy and smithing techniques. 

In the baseline of this system you may take unrefined ore and turn that into a refined ingot of the base metal or alloy into a mixed ingot.  From those refined materials you may then cast a number of different components. For example you could cast some gold into a pickaxe head, which can then be combined with a tool rod and binding to create a pickaxe. 

The defined ores at this point consist of: copper, tin, iron, silver and gold. However this is planned to be expanded on. A document with metals and alloy combinations is planned to be produced for reference.  
Products created will obtain data from the materials they are created from. Such as the metal created from, which will affect the statistics, such as a dense metal being good for damage on a sword, but will be heavy and will decrease the swing speed.

Product durability has been discussed as a potential idea, and may be of use, as it will encourage the player to produce more armor and weapons for themselves, instead of relying on just one set. Losing random items upon death in the dungeon has also been discussed, and would increase this cycle.

Types of equipment that can be made:
Defensive armor, such as helmet, boots, gloves, chestplate and leggings, can reduce the amount of damage taken, as well as applying additional effects, such as increasing the speed dependent on the lightness of the armor. Resistance to specific attacks could be a potential idea, such as armor creased from a fiery metal, may have a residence from flame-based effects.
Sword, main attacking weapon, different metals may affect the speed and damage, and may produce different effects in combat.
Shield, to defend against attacks
Hammer, to break walls in a dungeon.
Bow and arrows (potential idea). More flexible  metals may provide a faster draw speed, and the metal tip used on the arrows will affect speed and damage,


Ideas produced by the group for further development:
More types of equipment
Expand upon the types of ores and alloys that can be obtained. 
Effects applicable in accordance to the materials used
More methods of metallurgy, such as ore purification
Methods to increase the produced refined materials from ores
Upgrades to your crafting stations to craft more complex and powerful items
Styles for weapon/armor pieces, like roman/celtic/egyptian-esque looks

Management
The fourth mechanic the game will contain is that of management simulation. Once the player has produced a surplus of equipment or refined materials they may then sell them in exchange for the game's currency. 

The player will be able to sell their items. The baseline of selling within your store has yet to be defined. However a material value can be calculated with the materials used to create the product, which could be a baseline to the price.

Ideas produced by the group for further development:
Passportout style customers and purchasing
The player is also able to take requests where they must produce a specific item. This may be within a timeframe , and the end product will yield a higher recompense than conventionally selling the item.

Socializing
The final mechanic within the game will be the social simulation, which will be developed in the latter part of our development timeframe. Within this, the player will have the capacity to interact with the NPCs available in the game with the intent of pursuing a replication of friendship. For the baseline the specific workings of this mechanic are yet to be defined due to a desire to focus on other elements first. The mechanics would be available for each of the defined NPC’s we have for now.

Ideas produced by the group for further development:
A friendship status, which through gifting, interaction, and completing tasks, the player can unlock different dialogs with the player, and newer more challenging tasks. This may open up methods to obtaining specific materials that are difficult to find in the dungeon, and may even allow products to be sold for a higher base price to the characters.
Romantic relationships
Character events
Item creation quests, for example, a village member broke their sword, can you create one out of xx material for them? This may increase the relationship between you and the character, and will result in a reward which may be higher than simply selling the created product.


5. Visuals

3D Assets
The main form of visual graphics we will be using are 3D models. This is due to our teams strength at 3D modeling 

Link to visual moodboard

These models will be produced in the voxel art style using the free software “VoxEdit”. Blender will be used as an additional modeling programming, which will be used for things such as setting up armature, basic animations, and basic modeling, 

At the time of writing the team has just started the process of defining a specific style and working to understand the tools available in creating voxel-based assets, please see the visual moodboard for more information and notes on our style.

The reasons as to why we have chosen this artstyle can be divided into two parts. The first would be the consideration of game titles that are similar to what we hope to achieve and their visual styles. In these titles such as “Stardew Valley” and Minecraft there is a certain amount of similarity in visual style and we hope to capitalize on said similarity in these games while innovating in other areas to create our own identity. The second reason for the stylistic choice would be due to the skillset found within our group. Our design team has a particular preference for 3D modeling and experience with either pixel art a similar 2D style or voxel art prior to the project. Inspirations for the style can be seen as follows:

Our inspirations for art style:
Brendan Sullivan: https://twitter.com/artofsully. - Some of his older work has been removed from his portfolio, however can be found in this pinterest search: Link to older work
Cube World: https://picroma.com


Baseline assets
Mountain environmental asset
Clouds
Individual platforms
Platform stairways
Player model(iteration 1)
Mine entrance 
Player home
Crafting stations
Player home furnishing
NPC/village buildings
Potion store
Materials store
Builders workshop
Rival store
Tavern
Village hall
NPC/village furnishings
NPC models
One relevant to each building bar player home
Customers
Mine interior for level 1
Mineable ore for level 1
Basic enemy types
Basic crafting components
Basic tool components



Potential assets
Any assets that would be required by further development of mechanics
Additional enemies
Additional crafting stations and upgrades
Additional layers too cave with additional ores and so on
Buildings upgrade (player home and NPC housing)
Decorative objects
Objects for player customization
Objects for character customization



Animation
(A description as to the methods of animation that will take place and relevant assets that will receive assets ) (I’ll let bez do that one since he’s more of an animator than I am)

2D Assets
(A description of the processes that will be used for 2d assets and a list of baseline and potential assets) (we need to talk 2d assets)

Shaders

Sky shaders:

In our game there will be a day night cycle. Therefore we need a night and day shader. In a previous project I created a skybox shader for night time, which generates stars onto a gradient background. I plan to translate this into a shader graph to recreate this. My current plan for day and night cycle. Is for a current time to input into the shader, which will help my shader Lerp between the day and night values (by lerping the transparency of the stars, the colors of the different times). The daytime shader should also be fairly simple, with a gradient sky of different colors. I plan to have a sun and moon in the game, however this most likely won't be achieved with shaders, and probably by having a sun and moon which rotates around the main camera’s position. 

Hot Metal Shader:
In the style of blacksmithing, I want to create a shader to reflect the idea of metal heating up. Therefore I will input the position of a gameobject into the shader. From there it will get the distance from that point, and use an appropriate point from a gradient as an emission.

Molten Metals:
It would be useful to have a molten metal looking shader, for when the metals are in a liquid state during the crafting process. 

Cloud shaders:
I would like to create a cloud system such as this link: http://astroukoff.blogspot.com/2019/09/clouds-shader-breakdown.html. Since our setting is on a mounting above the clouds, it would be best to have clouds generated to cover the scenery below and fit with  our style. 




6. Narrative Elements

Setting
At the time of writing the team has decided to use a placeholder setting in order to ease the early production of assets for the placement presentation. The setting the game takes place in is a high fantasy world with minor steampunk elements and a soft approach to world building. The player lives in a small village built atop a tall, lone standing mountain above the clouds. In this mountain disconnected from the wider world sits an enchanted mine in which the player delves in search of ore for his smithy. The mine is famous for having the highest quality magic ore for swords and armor but few dare to enter due to the great danger in the mine. The main focus of the setting is on the village and the wider world will on occasion be hinted at or referred to but never directly addressed or expanded upon.

Characters
(An overview of the intended use of characters along with a list of said characters with relevant descriptive details provided)
The game will feature a number of NPC characters aside from the player. These characters will serve an amount of utilitarian function to the player in the form of necessary services. The NPC’s are intended to be written as more than two dimensional characters with some amount of personality and impersonal interaction being desired. The NPCs additionally will act in accordance to an ingame time schedule. The specific mechanics of this are yet to be determined by the group.

List of NPCs
Potion seller
Materials merchant
Builder
Rival
Tavern keeper
Village elder



7. Programming

Relevant Technology and process
(This ones for Phoebe and Connor to describe what they intend to do with code within the project with relative detail)


8. Miscellaneous Elements

Miscellaneous Audio
In order to create a further sense of immersion a number of sound effects will be produced. In order to produce these sound effects a mixture of recorded and MIDI sounds will be created and edited in Waveform studio to produce the desired effects.  For MIDI sounds there will be a focus on the use of the Arturia suite and the Spitfire “Labs” due to licenses. 

List of sound effects
Footsteps
Doors
Wind
Character mumble
Enter mine
Mine ambiance
Attack sfx
Enemy sfx



Music
In order to convey certain moods within areas, music will be composed for specific areas. In order to do this a number of live and MIDI recordings of instrumentation will be made, then edited in Waveform studio. For MIDI sounds there will be a focus on the use of Famitracker, the Arturia suite and the Spitfire “Labs” due to licenses.  The tracks will be produced for each relevant NPC we have at the moment within the town and then for areas that would have been deemed to benefit for them. Character themes will be produced relevant to the individual so they may be used in any scenario or area relevant to the character. Area themes will be applied to areas where there is no NPC whose theme is applicable and would benefit tonaly from music.

Track List
Home day
Home night
Town theme day
Town theme night
Potion seller theme
Material merchant theme
Builders theme
Rivals theme
Tavern Keeper theme
Village elder theme
Mine Level one theme




9. Development

Stages of Development
For our development we have set in place a series of milestones for the development of our baseline design. This was done in order to provide short term goals for our development and guide our production. As for developments after the initial design of the game we intend to evaluate that based on ideas that have developed over time of the initial development along with our remaining period of time.

Milestones:
Placement presentation
Establishment of a new concept
Early groundwork of conceptual design
Production and experimentation of early assets
Production and showing of a presentation
Receiving relevant feedback

Playable demo one (expected for early January) 
The production of the players home to a first iteration
The whitebox of the village displaying the first iteration of models created
Generation of procedural caves, with a basic dungeon system. For example, monster spawning.
First iteration of crafting mechanic, which will include basic smelting and alloying. 
Basic shaders. Such as sky and metal shaders.
First iteration of core scripts, such as movement, camera movement
First iteration of UI, such as inventory systems and menus.

Refinement of demo one (Expected for January 17th) 
Review and polishing of elements produced in demo one
Review design
Expand upon the crafting system.
NPC interaction. With dialog and questing system
Dungeon Monster AI
Player animations
Dungeon level themes

Playable demo two (Expected for February 17th) 
Creation of combat mechanic
Creation of management mechanic



Refinement of Demo two (Expected for March 17th) 
Refinement of combat and management mechanics 
Implementation of combat and management mechanics into playable demo

Playable demo three (Expected for April 17th) 



Refinement of demo three (Expected for May 17th) 
Bug fixes
Make preparations for Comx
Begin Kickstarter

ComX ( Expected during May)
Presentation of baseline mechanics all playable in a singular demo



Beginning of marketing ( Expected for late May to  Early  June )
Begin online campaigns in attempts to reach an audience along with the creation of personal and team brand

Beginning of talks with publishers ( Expected for mid to late June )
At this point we will attempt to begin the creation of connections with those in the publishing sector of the games industry

Development of existing mechanics 
Take ideas for further development of mechanics and concept

Refinement of additions
Review and polish of additions
At the end of this we should have our final state of gameplay bar minor tweaks or bugs that could not be done in time for ComX

Game Launch ( Expected 
We aim to take our project onwards from the third demo, into a product to release on steam in June. Therefore the month between will be preparing for our launch.

Timeframe
The development will use a timeline based around the aforementioned milestone to give a soft deadline to them with the intent of providing more pressure to the team to make progress as needed. Time points for other miscellaneous points in time have been added to the timeline as well to give context to those ongoing actions.

Timeline
Start of new concept - November 17th 2021
Beginning of development -  November 19th 2021
Placement presentation - November 24th 2021
Playable demo one - December 17th 2021
Refinement of demo one - January 17th 2022
Playable demo two - February 17th 2022
Refinement of Demo two - March 17th 2022
Playable demo three - April 17th 2022
Refinement of demo three - May 17th 2022
Beginning of marketing
Beginning of talks with publishers
Development of existing mechanics
Refinement of additions
ComX - May 2022
Refinement - in between ComX and release
Release - July 2022





Expansions of scale
To begin with our design will be aimed towards a baseline build. Once this baseline has been developed the team will then begin marketing and crowdfunding. Some weeks into this the team will then take the baseline to publishers. Once meetings with publishers have passed we will then take the various lists of how we can further develop the game into consideration with the remaining time till our intended release in July and ComX in May.

Working expectations
Due to issues faced within our first development in regards to interaction with the project the team has set inplace a set of measures with aid from our tutor to try and avoid this from happening in the future. The first measure is the use of the toggltrack site. To enforce our target hours, we have given our tutor access to the tracking software, so that any unaccounted hours can be discussed with someone outside of the organization. We will be making regular reports about the project with the intent to provide more pressure to put time into the project. Additionally to this we have set a flexible working time of 10AM to 3PM UK standard time to sit together in a Discord call sharing progress and discussing relevant matters. This action was put into place in order to create a feeling of community for the team alongside making organizing time for work easier for the team.

 Our target hours each week is 25 hours, and we intend for some flexibility due to members working or personal issues. However the project hours should not be under 20 without just reasoning. These hours can be divided into a target of 5 hours a day for each working day in the week.  

10. Business

Marketing
Our team in order to aim and improve the success of our eventual sales will engage in digital marketing. Our focus will be on social media marketing, with the possibility of video marketing on video sharing platforms. During the later stages in our development, we plan to implement a kickstarter and email marketing list, in which we will be able to contact our customers through email to inform them of our game development. 

Once our first playable demo is completed, we will begin our social media marketing. We will share updates and screenshots of our game on platforms such as Twitter, Reddit as a focus.  On these platforms we will divulge trailers and gameplay footage about the game alongside developments of personal brands using individual accounts.


Finances
The current approach to finances is to avoid any relevant expenditure and use what existing equipment and software we already have access to. At the point where marketing begins, the team will begin a kickstarter to sell early access copies of the game and ask for donations. The goal of this is more an attempt to gauge interest though any funds provided will be held in a business account by a representative. Use of these funds will depend on a vote from the team.

Licensing of software
In order to avoid any legal issues the teams use of software to pieces in which provide free commercial licensing or that we have existing licenses to. If required we will use student licenses then manage payment for commercial licenses after release.

Game Engine and IDE
Unity - our game engine. This was chosen as our programmers have the most experience within this language, and the ease in which resources can be obtained for it.
Visual Studio - our chosen programming ide
Visual Code - for shader language editing
Asset Production:
VoxEdit - a good voxel modeling software, which handles texturing and rigging by default
Blender - for brief model editing or rigging
3DS Max - for brief model editing or rigging
Photoshop - our chosen image editing software
Sound software:
Waveform Studio
Spitfire “Labs”
Arturia Suite
Famitracker
Communication and organization
Discord - for team communication
Teams - for communication with the university
Toggle tracker - our time tracking software. All hours worked must be logged on here
Trello - for task lists with the date to be completed noted on, this can be consulted if any member is unsure of what work they have to complete.
Google docs, slides and drawings - for producing group documents, such as the design document or project moodboards. 

Publishing
After our marketing and publishing have begun we will begin to try and approach individuals within the games publishing sector. Our intention with this is not so much to receive an actual publishing deal but to begin to gain experience of approaching those in that area and begin the development of connections. If in the event we do receive an offer for publishing we will consider and accept it depending on the terms. In the event we do not have the aid of a publisher we will pursue avenues of self publishing on online PC platforms.


Links to google folders
https://drive.google.com/drive/folders/1lYxIhP5_UUcgd-Ml53aMasU-FUw7fJp_?usp=sharing 

