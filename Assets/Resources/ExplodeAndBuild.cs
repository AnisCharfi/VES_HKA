using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAndBuild : MonoBehaviour
{
    public float moveDistance = 0.01f; // Distance de d�placement
    public float moveDuration = 2.0f; // Dur�e du d�placement
    private bool explode = false; // Indicateur pour l'explosion
    private bool build = false; // Indicateur pour r�assembler

    // Enum�ration pour les directions de mouvement
    public enum Direction { Up, Down, Left, Right }

    // Classe pour stocker chaque �l�ment et sa direction de mouvement
    [System.Serializable]
    public class Element
    {
        public Transform transform;
        public Direction direction;
        [HideInInspector] public Vector3 initialPosition; // Stocker la position initiale mais la cacher dans l'inspecteur
    }

    public Element[] elements; // Exposer les �l�ments dans l'inspecteur

    void Start()
    {
        // Initialiser les positions initiales des �l�ments
        foreach (var element in elements)
        {
            if (element.transform != null)
            {
                element.initialPosition = element.transform.position;
            }
        }
    }

    void Update()
    {
        if (explode)
        {
            // Si l'explosion est d�clench�e, d�placer les �l�ments
            foreach (var element in elements)
            {
                if (element.transform != null)
                {
                    StartCoroutine(MoveComponent(element.transform, element.direction));
                }
            }
            explode = false; // R�initialiser l'indicateur d'explosion
        }

        if (build)
        {
            // Si la reconstruction est d�clench�e, d�placer les �l�ments � leur position initiale
            foreach (var element in elements)
            {
                if (element.transform != null)
                {
                    StartCoroutine(MoveComponentToInitialPosition(element.transform, element.initialPosition));
                }
            }
            build = false; // R�initialiser l'indicateur de reconstruction
        }
    }

    // M�thode pour d�clencher l'explosion via un bouton
    public void ExplodeComponents()
    {
        explode = true;
    }

    // M�thode pour r�assembler les composants via un bouton
    public void BuildComponents()
    {
        build = true;
    }

    // Coroutine pour d�placer un composant
    private IEnumerator MoveComponent(Transform component, Direction direction)
    {
        Vector3 startPosition = component.position;
        Vector3 endPosition = startPosition + GetMoveVector(direction);

        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            component.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        component.position = endPosition;
    }

    // Coroutine pour d�placer un composant � sa position initiale
    private IEnumerator MoveComponentToInitialPosition(Transform component, Vector3 initialPosition)
    {
        Vector3 startPosition = component.position;

        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            component.position = Vector3.Lerp(startPosition, initialPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        component.position = initialPosition;
    }

    // Obtenir le vecteur de d�placement pour une direction donn�e
    private Vector3 GetMoveVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector3.up * moveDistance;
            case Direction.Down:
                return Vector3.down * moveDistance;
            case Direction.Left:
                return Vector3.left * moveDistance;
            case Direction.Right:
                return Vector3.right * moveDistance;
            default:
                return Vector3.zero;
        }
    }
}
