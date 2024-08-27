using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAndBuild : MonoBehaviour
{
    public float moveDistance = 0.01f; // Distance de déplacement
    public float moveDuration = 2.0f; // Durée du déplacement
    private bool explode = false; // Indicateur pour l'explosion
    private bool build = false; // Indicateur pour réassembler

    // Enumération pour les directions de mouvement
    public enum Direction { Up, Down, Left, Right }

    // Classe pour stocker chaque élément et sa direction de mouvement
    [System.Serializable]
    public class Element
    {
        public Transform transform;
        public Direction direction;
        [HideInInspector] public Vector3 initialPosition; // Stocker la position initiale mais la cacher dans l'inspecteur
    }

    public Element[] elements; // Exposer les éléments dans l'inspecteur

    void Start()
    {
        // Initialiser les positions initiales des éléments
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
            // Si l'explosion est déclenchée, déplacer les éléments
            foreach (var element in elements)
            {
                if (element.transform != null)
                {
                    StartCoroutine(MoveComponent(element.transform, element.direction));
                }
            }
            explode = false; // Réinitialiser l'indicateur d'explosion
        }

        if (build)
        {
            // Si la reconstruction est déclenchée, déplacer les éléments à leur position initiale
            foreach (var element in elements)
            {
                if (element.transform != null)
                {
                    StartCoroutine(MoveComponentToInitialPosition(element.transform, element.initialPosition));
                }
            }
            build = false; // Réinitialiser l'indicateur de reconstruction
        }
    }

    // Méthode pour déclencher l'explosion via un bouton
    public void ExplodeComponents()
    {
        explode = true;
    }

    // Méthode pour réassembler les composants via un bouton
    public void BuildComponents()
    {
        build = true;
    }

    // Coroutine pour déplacer un composant
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

    // Coroutine pour déplacer un composant à sa position initiale
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

    // Obtenir le vecteur de déplacement pour une direction donnée
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
