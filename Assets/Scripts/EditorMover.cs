using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;

		//todo comment: Что произойдёт, если _delay > _duration?
		// объект никогда не сохранит свою позицию
		[field: SerializeField, Range(0.2f, 1f)]
		private float _delay = 0.5f;
		[field: SerializeField, Min(0.2f)]
		private float _duration = 5f;

		private void Start()
		{
			//todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
			// потому что start вызовется один раз в самом начале, в отличии от update
			if (_duration <= _delay)
			{
				_duration = _delay * 5;
			}
			_save = GetComponent<PositionSaver>();
			_save.Records.Clear();
		}

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}
			
			//todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
			// _delay нельзя изменять, иначе мы потеряем его изначальное значение
			_currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
                    //todo comment: Для чего сохраняется значение игрового времени?
                    // чтобы отсчет deltaTime производился от предыдущего сохранения
                    Time = Time.time,
				});
			}
		}
	}
}