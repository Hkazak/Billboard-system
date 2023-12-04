import "@tomtom-international/web-sdk-maps/dist/maps.css";
import tt from "@tomtom-international/web-sdk-maps";
import services from "@tomtom-international/web-sdk-services";
import React, {useEffect, useRef, useState} from "react";
import {render} from "@testing-library/react";
import ShortBillboardInfo from "./ShortBillboardInfo";

function Map({markBillboards, onSelectBillboard}) {
    const mapContainer = useRef(null);
    const [mapLng, setMapLng] = useState(76.90991436305302);
    const [mapLat, setMapLat] = useState(43.234824749685316);
    const [mapZoom, setMapZoom] = useState(12);
    const markers = useRef({});
    const markedBillboards = useRef(new Set());
    const map = useRef({});
    const apiKey = 'n80YGBrqqDTp0tuSQsbh5OCuXu9cSwuJ';

    async function createMarkerForBillboard(billboard) {
        const element = <ShortBillboardInfo billboard={billboard} onSelect={onSelectBillboard}/>
        const response = await services.services.fuzzySearch({
            key: apiKey,
            language: 'ru-RU',
            countrySet: 'kz',
            query: `Алматы ${billboard.address}`
        });
        let lng = mapLng;
        let lat = mapLat;
        for (const position of response.results.map(e => e.position)) {
            const samePointIndex = Object.values(markers.current).map(e => e.getLngLat()).findIndex(e => e.lng === position.lng && e.lat === position.lat);
            if (samePointIndex !== -1) {
                continue;
            }
            lng = position.lng;
            lat = position.lat;
        }
        const marker = new tt.Marker({
            anchor: 'top'
        });
        return marker
            .setLngLat([lng, lat])
            .setPopup(new tt.Popup().setDOMContent(render(element).container));
    }

    useEffect(() => {
        map.current = tt.map({
            key: apiKey,
            container: mapContainer.current,
            center: [mapLng, mapLat],
            zoom: mapZoom
        });

        return () => map.current.remove();
    }, [mapLng, mapLat, mapZoom]);
    return (
        <div className="map-container" ref={mapContainer}>
            {markBillboards.map(e => {
                if (!markedBillboards.current.has(e.id)) {
                    createMarkerForBillboard(e)
                        .then(e1 => {
                            e1.addTo(map.current);
                            console.log('added', e.id);
                            markedBillboards.current.add(e.id);
                        })
                }
                return <div id={`billboard-${e.id}`} key={e.id}/>
            })}
        </div>
    );
}

export default Map;