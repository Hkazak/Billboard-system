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
        map.current.on('move', (e)=>{
            const center = e.target.getCenter();
            setMapLat(center.lat)
            setMapLng(center.lng);
            setMapZoom(e.target.getZoom());
        });
        map.current.on('zoom', (e)=>{
            const center = e.target.getCenter();
            setMapLat(center.lat)
            setMapLng(center.lng);
            setMapZoom(e.target.getZoom());
        });

        for(const billboard of markBillboards)
        {
            createMarkerForBillboard(billboard)
                .then(e=>e.addTo(map.current));
        }

        return () => {
            map.current.off('zoom');
            map.current.off('move');
            map.current.remove();
        };
    }, [markBillboards]);
    return (
        <div className="map-container" ref={mapContainer}>
        </div>
    );
}

export default Map;