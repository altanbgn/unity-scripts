using System.Collections;

namespace CovertPath.Interfaces {
	public interface IAttributes {
		void TakePhysicalDamage(float damage);
		void TakeMagicDamage(float damage);
		IEnumerator Die();
	}
}