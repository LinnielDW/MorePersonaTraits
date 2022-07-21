#More Persona Trait Templates
I've included this README as a way for other devs to implement their own weapon persona traits using the things I've found, without having to redo all the research again.

Within the Rimworld codebase, weapon persona traits or link traits are referred to as WeaponTraits. 

It should also be noted that as of writing this (Rimworld 1.3), the only way these traits spawn is on persona weapons (added as part of the Royalty Expansion).

TODO: fill this in more 

##Examples
###WeaponTraitTemplateExplaination
This is a base template, giving a brief overview of each field and what you can do with it. If you have any extra questions, please check out the specific examples or just ask me.
###Bonded
Contains a complete example of a WeaponTrait that gives your pawn some kind of permenant benefit, for as long as the Pawn and persona weapon remain bonded.

**For consistancy, the NeverBond exclusionTag should be used with such weapons.**
###Held
Contains a complete example of a WeaponTrait that gives your pawn a benefit ONLY when the persona weapon is held.
###On Hit
Contains examples of functionality for when a persona weapon deals damage to an enemy. 

On-hit functionality was not a feature present in the Royalty expansion and is new to this mod. 
If you wish to use this, you can add this mod as a dependancy. See the OnHitTemplate for more information.

#Delete
##Bonus Fields
In this folder, you will find files which contain lists that you can use to assign to your weapon traits.
###statOffsets
This is a list of stats that can be used to give your pawn a statistical increase.