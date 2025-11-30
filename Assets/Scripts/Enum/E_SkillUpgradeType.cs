
public enum E_SkillUpgradeType 
{
    None,

    // Dash Tree
    Dash, // Dash to avoid damage
    Dash_CloneOnStart, // Create a clone when dash starts
    Dash_CloneOnStartAndArrival, // Create a clone when dash starts and ends
    Dash_ShardOnStart, // Create a shard when dash starts
    Dash_ShardOnStartAndArrival, // Crearte a shard when dash starts and ends

    // Shard Tree
    Shard, // The shard explodes when touched by an enemy or when time goes up  
    Shard_MoveToEnemy, // Shard will move towards nearest enemy
    Shard_Multicast, // Shard ability can have up to N charges. You can cast them all in a row
    Shard_Teleport, // You can swap places with the last shard you created
    Shard_TeleportHPRewind, // When you swap places with shard, your HP% is the same as it was when you created shard

    // Sword Tree
    SwordThrow, // You can throw sword to damage enemies from range
    SwordThrowSpin, // Sword will spin at one point and damage enemies like a chainsaw
    SwordThrowPierce, // Sword will pierce a certain amount of targets
    SwordThrowBounce, // Sword will bounce between enemies 
    
    // Time Echo
    TimeEcho, // Create a clone of a player. It can take damage from enemies
    TimeEcho_SingleAttack, // Time Echo can perform a single attack
    TimeEcho_MultiAttack, // Time Echo can perform multiple attacks
    TimeEcho_ChanceToDuplicate, // Time Echo has a chance to create another time echo when attacking
    TimeEcho_HealWisp, // When time echo dies it creates a wisp that flies towards the player to heal
    TimeEcho_CleanseWisp, // Wisp will now remove negative effects from player
    TimeEcho_CooldownWisp, // Wisp will now reduce cooldown of all skills by a few secondss

    // Domain Expansion
    Domain_SlowDown, // Create an area in which you slow down enemies by 90% - 100%. Player can move and fight freely
    Domain_EchoSpam, // Player can no longer move, but can spam enemies with time echo ability
    Domain_ShardSpam // Player can no longer move, but can spam enemies with shard ability

}
