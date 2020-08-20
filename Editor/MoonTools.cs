using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* 
 * Copyright(C) 2020 M.O.O.N 
 * Description: Handles some super annoying tasks for you, especially for animators.
 * Discord: M.O.O.N#8008
 * Version: 1.0.0 (07/05/2020)
 */

public class MoonTools : Editor
{

    [MenuItem("M.O.O.N Tools/Configuration")]
    public static void ShowConfigurationDialog()
    {
        EditorUtility.DisplayDialog("M.O.O.N Tools", "This Menuentry is a stub for now.", "Okay");
        Debug.Log("This method is a stub right now.");
    }

    [MenuItem("M.O.O.N Tools/Reset Config")]
    public static void ResetConfig()
    {
        PlayerPrefs.DeleteKey("MoonDefaultSettings");
        EditorUtility.DisplayDialog("M.O.O.N Tools", "Configuration has been successfully reset", "Okay");
    }

    //Hierarchy Menu Extensions
    [MenuItem("GameObject/Particle System (Animation Ready)", false, 0)]
    private static void ParticleSystemButFixed()
    {
        GameObject temporaryGo = new GameObject("Particle System",typeof(ParticleSystem));
        temporaryGo.name = "MoonParticleSystem";
        ParticleSystemRenderer ps = temporaryGo.GetComponent<ParticleSystemRenderer>();
        var color = Color.white;
        Material m = new Material(Shader.Find("Particles/Additive Intensify"));
        m.SetColor("_TintColor", new Color(1, 1, 1));
        ps.sharedMaterial = m;
        ps.maxParticleSize = 999999999f;
        Selection.activeGameObject = temporaryGo;
    }


    [MenuItem("GameObject/Buffer Particle", false, 0)]
    private static void BufferParticle()
    {
        //ContainerObject
        GameObject bufferContainer = new GameObject("Buffer Container");
        //Main Particle object
        GameObject temporaryGo = new GameObject();
        temporaryGo.transform.SetParent(bufferContainer.transform);
        temporaryGo.name = "MoonBufferParticle";
        temporaryGo.AddComponent<ParticleSystem>();
        ParticleSystemRenderer psRenderer = temporaryGo.GetComponent<ParticleSystemRenderer>();
        psRenderer.enabled = false;
        ParticleSystem psAct = temporaryGo.GetComponent<ParticleSystem>();
        var main = psAct.main;
        main.startLifetime = 0.01f;
        main.startSpeed = 0.1f;
        main.playOnAwake = true;
        main.loop = false;
        var emissionTab = psAct.emission;
        var subEmittersModule = psAct.subEmitters;
        emissionTab.rateOverTime = 0;
        emissionTab.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(0, 1)
            });

        //SHAPE _ MAIN

        var shapesTab = psAct.shape;
        shapesTab.enabled = false;


        //Subemitter Config starts here
        GameObject subSystemGO = new GameObject("SubEmitter");
        var subParticleSystem = subSystemGO.AddComponent<ParticleSystem>();
        subSystemGO.GetComponent<ParticleSystemRenderer>().material = new Material(Shader.Find("!M.O.O.N/Additive Intensify"));
        var subMainModule = subParticleSystem.main;
        subMainModule.startSize = 0.25f;
        subMainModule.playOnAwake = false;
        subMainModule.loop = false;
        var emissionModule = subParticleSystem.emission;


        subSystemGO.transform.SetParent(temporaryGo.transform);
        subEmittersModule.enabled = true;
        subEmittersModule.AddSubEmitter(subParticleSystem, ParticleSystemSubEmitterType.Death, ParticleSystemSubEmitterProperties.InheritNothing);

        Transform currentParent = temporaryGo.transform.parent;
        subParticleSystem.gameObject.transform.SetParent(currentParent);

        ParticleSystem subObjEmission = subParticleSystem.GetComponent<ParticleSystem>();
        var subObjEmissionMain = subObjEmission.emission;

        subObjEmissionMain.SetBursts(
            new ParticleSystem.Burst[]{
                new ParticleSystem.Burst(1, 100)
            });

        subObjEmissionMain.rateOverTime = 0;
        var SubObjShape = subObjEmission.shape;

        SubObjShape.shapeType = ParticleSystemShapeType.Sphere;

        var color = Color.white;
        Material m = new Material(Shader.Find("!M.O.O.N/Additive Intensify"));
        m.SetColor("_TintColor", new Color(1, 1, 1));
        psRenderer.sharedMaterial = m;
        psRenderer.maxParticleSize = 999999999f;
        Selection.activeGameObject = temporaryGo;
    }

    public GameObject createParticleSystemWithSetup()
    {
        GameObject temporaryGo = new GameObject();
        temporaryGo.name = "MoonParticleSystem";
        temporaryGo.AddComponent<ParticleSystem>();
        ParticleSystemRenderer ps = temporaryGo.GetComponent<ParticleSystemRenderer>();
        var color = Color.white;
        Material m = new Material(Shader.Find("!M.O.O.N/Additive Intensify"));
        //Debug.Log("[MoonToolsV2] Important shader was not found! \"!M.O.O.N/Additive Intensify\"");
        m.SetColor("_TintColor", new Color(1, 1, 1));
        ps.sharedMaterial = m;
        ps.maxParticleSize = 999999999f;
        return temporaryGo;
    }



}
