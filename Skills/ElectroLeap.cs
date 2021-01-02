using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using CovertPath.Interfaces;
using CovertPath.Mechanics;

namespace CovertPath.Skills {
	public class ElectroLeap : Skill {
		public override IEnumerator Initiate(GameObject player) {
			NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
			// Consumes mana
			_playerAttributes.ConsumeMana(manaCost);
			// Gets the default speed of player
			float defaultSpeed = agent.speed;
			// Plays particle and audio of skill
			if (skillAudio != null)
				skillAudio.Play();
			if (skillParticle != null)
				skillParticle.Play();
			// Waiting for leave fog behind on particle
			yield return new WaitForSeconds(0.1f);
			// Warps 4 unit front
			agent.Warp(player.transform.position + (player.transform.forward * 3f));
			// Adds speed to the player
			agent.speed += 3f;
			// when skill duration times out
			yield return new WaitForSeconds(skillDuration);
			// Removes the speed
			agent.speed = defaultSpeed;
		}
	}
}